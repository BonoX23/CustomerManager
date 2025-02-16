using Domain.Address;

namespace Application.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto CustomerDto { get; set; }

        public static Address MapToEntity(AddressDto dto)
        {
            return new Address
            {
                Id = dto.Id,
                Place = dto.Place,
                Neighborhood = dto.Neighborhood,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CustomerId = dto.CustomerId,
                Customer = dto.CustomerDto != null ? CustomerDto.MapToEntity(dto.CustomerDto) : null
            };
        }

        public static AddressDto MapToDto(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Place = address.Place,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                CustomerId = address.CustomerId,
                CustomerDto = address.Customer != null ? CustomerDto.MapToDto(address.Customer) : null
            };
        }
    }
}