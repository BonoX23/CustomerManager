using Application.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface ICustomerService
    {
        Task<Tuple<string, string>> AddAsync(CreateCustomerRequest customerRequest);
        Task UpdateAsync(int userId, int customerId, CreateCustomerRequest customerRequest);
        Task DeleteAsync(int userId, int customerId);
        Task<CustomerResponse> GetByIdAsync(int userId, int customerId);
    }
}
