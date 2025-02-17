using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports;
using Application.Requests;

namespace CustomerManager.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class AddressController : MainController
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo logradouro ao cliente
        /// </summary>
        /// <param name="addressRequest"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("address/{customerId}")]
        public async Task<IActionResult> AddAddress(int customerId, [FromBody] CreateAddressRequest addressRequest)
        {
            var result = await _service.AddAsync(UserLoggedId, customerId, addressRequest);
            return Ok(result);
        }

        /// <summary>
        /// Buscar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("address/{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var result = await _service.GetByIdAsync(UserLoggedId, addressId);
            return Ok(result);
        }

        /// <summary>
        /// Buscar logradouros de um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("addresses/{customerId}")]
        public async Task<IActionResult> GetAddressesByCustomerId(int customerId)
        {
            var result = await _service.GetByCustomerIdAsync(UserLoggedId, customerId);
            return Ok(result);
        }

        /// <summary>
        /// Deletar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("address/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            await _service.DeleteAsync(UserLoggedId, addressId);
            return Ok(new Tuple<string>("Logradouro deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um logradouro de um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="addressId"></param>
        /// <param name="addressRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("address/{customerId}/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int customerId, int addressId, [FromBody] CreateAddressRequest addressRequest)
        {
            await _service.UpdateAsync(UserLoggedId, customerId, addressId, addressRequest);
            return Ok(new Tuple<string>("Logradouro atualizado com sucesso"));
        }
    }
}
