using Domain.Customer;

namespace Application.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public List<AddressDto> AddressesDto { get; set; }
        public List<UserDto> UsersDto { get; set; }

        public static Customer MapToEntity(CustomerDto dto)
        {
            return new Customer
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Logo = dto.Logo,
                Addresses = dto.AddressesDto.Select(a => AddressDto.MapToEntity(a)).ToList(),
                Users = dto.UsersDto.Select(u => UserDto.MapToEntity(u)).ToList()
            };
        }

        public static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Logo = customer.Logo,
                AddressesDto = customer.Addresses.Select(a => AddressDto.MapToDto(a)).ToList(),
                UsersDto = customer.Users.Select(u => UserDto.MapToDto(u)).ToList()
            };
        }
    }
}
