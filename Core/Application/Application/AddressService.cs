using Application.Ports;
using Application.Requests;
using Application.Dtos;
using Application.Responses;
using Domain.Ports;

namespace Application
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;

        public AddressService(IAddressRepository addressRepository, ICustomerRepository customerRepository)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Tuple<string>> AddAsync(int userId, int customerId, CreateAddressRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                throw new Exception("Cliente informado não existe");

            if (!customer.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            var addressDomain = AddressDto.MapToEntity(request.Data);

            addressDomain.Validate();

            await _addressRepository.AddAsync(addressDomain);
            await _addressRepository.UnitOfWork.Commit();

            return new Tuple<string>($"Logradouro adicionado ao cliente {customer.Name} com sucesso");
        }

        public async Task DeleteAsync(int userId, int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null)
                throw new Exception("Logradouro não existe");

            if (!address.Customer.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            await _addressRepository.DeleteAsync(address);
            await _addressRepository.UnitOfWork.Commit();
        }

        public async Task<AddressResponse> GetByIdAsync(int userId, int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null)
                throw new Exception("Logradouro não existe");

            if (!address.Customer.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            return new AddressResponse { Data = AddressDto.MapToDto(address) };
        }

        public async Task<IEnumerable<AddressResponse>> GetByCustomerIdAsync(int userId, int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                throw new Exception("Cliente informado não existe");

            if (!customer.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            var addresses = await _addressRepository.GetByCustomerIdAsync(customerId);

            return addresses.Select(address => new AddressResponse { Data = AddressDto.MapToDto(address) }).ToList();
        }

        public async Task UpdateAsync(int userId, int customerId, int addressId, CreateAddressRequest request)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null)
                throw new Exception("Logradouro não existe");

            if (!address.Customer.Users.Any(x => x.Id == userId) || address.CustomerId != customerId)
                throw new Exception("Ação não permitida, permissão negada!");

            address.Update(request.Data.Place, request.Data.Neighborhood, request.Data.City, request.Data.State, request.Data.ZipCode);

            address.Validate();

            await _addressRepository.UpdateAsync(address);
            await _addressRepository.UnitOfWork.Commit();
        }
    }
}
