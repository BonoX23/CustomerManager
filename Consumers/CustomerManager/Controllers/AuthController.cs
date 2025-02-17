using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Ports;
using Application.Requests;

namespace CustomerManager.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Autenticar
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Authenticate([FromBody] CreateUserRequest authRequest)
        {
            var response = await _service.AuthenticateAsync(authRequest);
            return Ok(response);
        }

        /// <summary>
        /// Atualizar Senha
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth/update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] CreateUserRequest updateRequest)
        {
            await _service.UpdatePasswordAsync(UserLoggedId, updateRequest);
            return Ok(new Tuple<string>("Senha atualizada com sucesso"));
        }
    }
}
