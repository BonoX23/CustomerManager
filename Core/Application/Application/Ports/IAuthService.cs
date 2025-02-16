using Application.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IAuthService
    {
        Task<UserResponse> AuthenticateAsync(CreateUserRequest createUserRequest);
        Task UpdatePasswordAsync(int userId, CreateUserRequest createUserRequest);
    }
}
