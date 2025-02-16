using Application.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IAddressManager
    {
        Task<Tuple<string>> AddAsync(int userId, int customerId, CreateAddressRequest request);
        Task UpdateAsync(int userId, int customerId, int addressId, CreateAddressRequest request);
        Task DeleteAsync(int userId, int addressId);
        Task<AddressResponse> GetByIdAsync(int userId, int addressId);
        Task<IEnumerable<AddressResponse>> GetByCustomerIdAsync(int userId, int customerId);
    }
}
