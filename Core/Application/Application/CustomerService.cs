using Application.Ports;
using Application.Requests;
using Application.Responses;
using Application.Dtos;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Identity;

namespace Application
{
    public class CustomerService : ICustomerManager
    {
        private readonly ICustomerRepository _repository;
        private readonly UserManager<User> _userManager;

        public CustomerService(ICustomerRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<Tuple<string>> AddAsync(CreateCustomerRequest customerRequest)
        {
            var customerDto = customerRequest.Data;

            if (!string.IsNullOrEmpty(customerDto.Email) && _repository.ValidateByEmail(customerDto.Email))
                return new Tuple<string>("E-mail já cadastrado");

            var passwordGuid = Guid.NewGuid().ToString().ToUpper().Split('-')[0];
            var userDomain = new User(customerDto.Email, passwordGuid);

            userDomain.PasswordHash = _userManager.PasswordHasher.HashPassword(userDomain, passwordGuid);

            var customerDomain = CustomerDto.MapToEntity(customerDto);
            customerDomain.Users.Add(userDomain);

            await _repository.AddAsync(customerDomain);
            await _repository.UnitOfWork.Commit();

            return new Tuple<string>($"Cadastro realizado com sucesso, utilize seu email como login e o código {passwordGuid} como senha");
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
            return new CustomerResponse { Data = customerDto };
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

            var updatedCustomer = CustomerDto.MapToEntity(customerDto);

            customerDomain.Name = updatedCustomer.Name;
            customerDomain.Email = updatedCustomer.Email;
            customerDomain.Logo = updatedCustomer.Logo;

            customerDomain.Addresses.Clear();

            foreach (var address in updatedCustomer.Addresses)
            {
                customerDomain.Addresses.Add(address);
            }

            await _repository.UpdateAsync(customerDomain);
            await _repository.UnitOfWork.Commit();
        }
    }
}
