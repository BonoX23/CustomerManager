using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Dtos
{
    public class UserDto : IdentityUser<int>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
            };
        }

        public static User MapToEntity(UserDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                UserName = dto.UserName,
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
