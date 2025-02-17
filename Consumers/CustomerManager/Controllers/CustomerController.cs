using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports;
using Application.Requests;

namespace CustomerManager.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class CustomersController : MainController
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo cliente
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerRequest customer)
        {
            var result = await _service.AddAsync(customer);
            return Ok(result);
        }

        /// <summary>
        /// Buscar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var result = await _service.GetByIdAsync(UserLoggedId, customerId);
            return Ok(result);
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _service.DeleteAsync(UserLoggedId, customerId);
            return Ok(new Tuple<string>("Cliente deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] CreateCustomerRequest customer)
        {
            await _service.UpdateAsync(UserLoggedId, customerId, customer);
            return Ok(new Tuple<string>("Cliente atualizado com sucesso"));
        }
    }
}
