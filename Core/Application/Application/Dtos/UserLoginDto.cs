using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Dtos
{
    public class UserLoginDto : IdentityUser<int>
    {
        public string Password { get; set; }

        public static UserLoginDto MapToDto(User user)
        {
            return new UserLoginDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public static User MapToEntity(UserLoginDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email
            };

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
            }

            return user;
        }
    }
}
