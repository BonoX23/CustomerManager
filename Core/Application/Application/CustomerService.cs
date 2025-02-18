using Application.Ports;
using Application.Requests;
using Application.Responses;
using Application.Dtos;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Identity;
using Domain.Customer;

namespace Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly UserManager<User> _userManager;

        public CustomerService(ICustomerRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<Tuple<string, string>> AddAsync(CreateCustomerRequest customerRequest)
        {
            var customerDto = customerRequest.Data;

            if (customerDto == null)
            {
                return new Tuple<string, string>("Os dados do cliente estão nulos.", null);
            }

            if (!string.IsNullOrEmpty(customerDto.Email) && _repository.ValidateByEmail(customerDto.Email))
            {
                return new Tuple<string, string>("E-mail já cadastrado", null);
            }

            var passwordGuid = Guid.NewGuid().ToString().ToUpper().Split('-')[0];
            var userDomain = new User(customerDto.Email, passwordGuid);

            userDomain.PasswordHash = _userManager.PasswordHasher.HashPassword(userDomain, passwordGuid);

            // Utilize o construtor da classe Customer
            var customerDomain = new Customer(customerDto.Name, customerDto.Email, customerDto.Logo);

            customerDomain.Users.Add(userDomain);

            if (customerDto.Addresses != null)
            {
                foreach (var addressDto in customerDto.Addresses)
                {
                    var address = AddressDto.MapToEntity(addressDto);
                    customerDomain.Addresses.Add(address);
                }
            }

            await _repository.AddAsync(customerDomain);
            await _repository.UnitOfWork.Commit();

            // Retorne null no primeiro item em caso de sucesso e o passwordGuid no segundo item
            return new Tuple<string, string>(null, passwordGuid);
        }



        public async Task DeleteAsync(int userId, int customerId)
        {
            var customerDomain = await _repository.GetByIdAsync(customerId);

            if (customerDomain == null)
                throw new Exception("Cliente não existe");

            if (!customerDomain.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            await _repository.DeleteAsync(customerDomain);
            await _repository.UnitOfWork.Commit();
        }

        public async Task<CustomerResponse> GetByIdAsync(int userId, int customerId)
        {
            var customerDomain = await _repository.GetByIdAsync(customerId);

            if (customerDomain == null)
                throw new Exception("Cliente não existe");

            if (!customerDomain.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            var customerDto = CustomerDto.MapToDto(customerDomain);
            return new CustomerResponse { Id = customerDomain.Id, Data = customerDto };
        }

        public async Task UpdateAsync(int userId, int customerId, CreateCustomerRequest customerRequest)
        {
            var customerDto = customerRequest.Data;
            var customerDomain = await _repository.GetByIdAsync(customerId);

            if (customerDomain == null)
                throw new Exception("Cliente não existe");

            if (!customerDomain.Users.Any(x => x.Id == userId))
                throw new Exception("Ação não permitida, permissão negada!");

            if (!string.IsNullOrEmpty(customerDto.Email) && _repository.ValidateByEmailUpdate(customerId, customerDto.Email))
                throw new Exception("E-mail já cadastrado");

            // Utilize o método Update da classe Customer
            customerDomain.Update(customerDto.Name, customerDto.Logo);

            customerDomain.Addresses.Clear();

            if (customerDto.Addresses != null)
            {
                foreach (var addressDto in customerDto.Addresses)
                {
                    var address = AddressDto.MapToEntity(addressDto);
                    customerDomain.Addresses.Add(address);
                }
            }

            await _repository.UpdateAsync(customerDomain);
            await _repository.UnitOfWork.Commit();
        }
    }
}
