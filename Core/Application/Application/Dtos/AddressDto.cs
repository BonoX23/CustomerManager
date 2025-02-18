namespace Application.Dtos
{
    public class AddressDto
    {
        public string Place { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public static Domain.Address.Address MapToEntity(AddressDto dto)
        {
            return new Domain.Address.Address(dto.Place, dto.Neighborhood, dto.City, dto.State, dto.ZipCode,0);
        }

        public static AddressDto MapToDto(Domain.Address.Address address)
        {
            return new AddressDto
            {
                Place = address.Place,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}
