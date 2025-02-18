using Domain.Customer;

namespace Application.Dtos
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public List<AddressDto> Addresses { get; set; }

        public static Customer MapToEntity(CustomerDto dto)
        {
            var customer = new Customer(dto.Name, dto.Email, dto.Logo);
            customer.Addresses = dto.Addresses.Select(a => AddressDto.MapToEntity(a)).ToList();
            return customer;
        }

        public static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                Name = customer.Name,
                Email = customer.Email,
                Logo = customer.Logo,
                Addresses = customer.Addresses.Select(a => AddressDto.MapToDto(a)).ToList()
            };
        }
    }
}
