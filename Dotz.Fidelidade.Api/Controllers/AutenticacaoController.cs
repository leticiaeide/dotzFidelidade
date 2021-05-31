using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Api.Controllers
{

    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _service;
        public AutenticacaoController(IAutenticacaoService service)
        {
            _service = service;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> Autenticacao([FromBody] UsuarioRequest request)
        {
            request.Validate();

            if (request.Valid)
            {
                return Ok(await _service.GerarTokenAsync(request));
            }

            return BadRequest(request.Notifications);
        }       
    }
}
