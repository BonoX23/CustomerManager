using Domain.Customer;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Users)
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<Customer> GetByUserIdAsync(int userId)
        {
            return await _context.Customers
                .Include(c => c.Users)
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == userId));
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Users)
                .Include(c => c.Addresses)
                .ToListAsync();
        }

        public bool ValidateByEmail(string email)
        {
            return _context.Customers.Any(c => c.Email == email);
        }

        public bool ValidateByEmailUpdate(int customerId, string email)
        {
            return _context.Customers.Any(c => c.Email == email && c.Id != customerId);
        }
    }
}
