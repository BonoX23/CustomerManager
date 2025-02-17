using Application.Dtos;
using Application.Ports;
using Application.Requests;
using Application.Responses;
using Domain.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly TokenConfiguration _tokenConfigurations;
        private readonly SymmetricSecurityKey _key;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

            _tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration.GetSection("TokenConfigurations")).Configure(_tokenConfigurations);

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfigurations.Key));
        }

        public async Task<UserResponse> AuthenticateAsync(CreateUserRequest createUserRequest)
        {
            var login = createUserRequest.Data.UserName;
            var password = createUserRequest.Data.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Login e Password são obrigatórios");
            }

            var result = await _signInManager.PasswordSignInAsync(login, password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Login ou Password inválidos");
            }

            var user = await _userManager.FindByNameAsync(login);
            var token = GenerateToken(user);

            return new UserResponse { Data = UserDto.MapToDto(user), Token = token };
        }

        public async Task UpdatePasswordAsync(int userId, CreateUserRequest createUserRequest)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var result = await _userManager.ChangePasswordAsync(user, createUserRequest.Data.Password, createUserRequest.Data.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Erro ao atualizar senha");
            }
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_tokenConfigurations.ExpireMinutes),
                SigningCredentials = creds,
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
