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
            var address = new Address(dto.Place, dto.Neighborhood, dto.City, dto.State, dto.ZipCode, dto.CustomerId)
            {
                Id = dto.Id
            };

            if (dto.CustomerDto != null)
            {
                address.Customer = CustomerDto.MapToEntity(dto.CustomerDto);
            }

            return address;
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
