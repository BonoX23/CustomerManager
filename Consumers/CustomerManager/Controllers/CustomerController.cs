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
        [AllowAnonymous]
        [Route("customer")]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerRequest customer)
        {
            var result = await _service.AddAsync(customer);

            if (result.Item1 != null)
            {
                return BadRequest(new { Message = result.Item1 });
            }

            return Ok(new { Message = "Cliente criado com sucesso!", LoginCode = result.Item2 });
        }

        /// <summary>
        /// Buscar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            try
            {
                var userId = /* Obtenha o ID do usuário autenticado aqui, por exemplo, via User.Claims */;
                var result = _service.GetByIdAsync(userId, customerId).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
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
