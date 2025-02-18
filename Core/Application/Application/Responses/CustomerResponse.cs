using Application.Dtos;

namespace Application.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public CustomerDto Data { get; set; }
        public AddressResponse Address { get; set; }
    }
}
