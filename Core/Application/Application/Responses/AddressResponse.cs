using Application.Dtos;

namespace Application.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public AddressDto Data { get; set; }
    }
}
