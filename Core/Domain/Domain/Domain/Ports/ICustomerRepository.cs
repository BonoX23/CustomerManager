using Domain.Contracts;

namespace Domain.Ports
{
    public interface ICustomerRepository : IRepository
    {
        Task<Customer.Customer> AddAsync(Customer.Customer customer);
        Task<Customer.Customer> UpdateAsync(Customer.Customer customer);
        Task<bool> DeleteAsync(Customer.Customer customer);
        Task<Customer.Customer> GetByIdAsync(int customerId);
        Task<Customer.Customer> GetByUserIdAsync(int userId);
        Task<IEnumerable<Customer.Customer>> GetAllAsync();
        bool ValidateByEmail(string email);
        bool ValidateByEmailUpdate(int customerId, string email);
    }
}
