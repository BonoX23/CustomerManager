using Domain.Contracts;

namespace Domain.Ports
{
    public interface IAddressRepository : IRepository
    {
        Task<Address.Address> AddAsync(Address.Address address);
        Task<Address.Address> UpdateAsync(Address.Address address);
        Task<bool> DeleteAsync(Address.Address address);
        Task<Address.Address> GetByIdAsync(int addressId);
        Task<IEnumerable<Address.Address>> GetByCustomerIdAsync(int customerId);
    }
}
