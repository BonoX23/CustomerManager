using Application.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IAuthManager
    {
        Task<UserResponse> AuthenticateAsync(CreateUserRequest createUserRequest);
        Task UpdatePasswordAsync(int userId, CreateUserRequest createUserRequest);
    }
}
