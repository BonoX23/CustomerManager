using Domain.Entities;

namespace Domain.Contracts
{
    public interface IAuthRepository : IRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> AuthenticateAsync(string login, string password);
        Task UpdatePasswordAsync(User user);
    }
}
