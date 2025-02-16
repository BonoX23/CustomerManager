using Application.Dtos;

namespace Application.Responses
{
    public class UserResponse
    {
        public UserDto Data { get; set; }
        public string Token { get; set; }
    }
}
