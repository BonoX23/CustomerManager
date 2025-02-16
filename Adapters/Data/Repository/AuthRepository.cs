using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly CustomerDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public AuthRepository(UserManager<User> userManager, CustomerDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string login, string password)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdatePasswordAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Error updating password");
            }
        }
    }
}
