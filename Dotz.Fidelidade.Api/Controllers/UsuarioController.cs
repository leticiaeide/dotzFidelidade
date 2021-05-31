using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Api.Controllers
{

    [Authorize(Roles = "Usuario")]
    [Route("api/[controller]")]    
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IUsuarioEnderecoService _usuarioEnderecoEntregaService;      

        
        public UsuarioController(IUsuarioService service,
                                IUsuarioEnderecoService usuarioEnderecoEntregaService) 
        {
            _service = service;
            _usuarioEnderecoEntregaService = usuarioEnderecoEntregaService;           
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Post([FromBody] UsuarioRequest request)
        {
            request.Validate();

            if (request.Valid)
            {
                return Ok(await _service.SalvarAsync(request));
            }

            return BadRequest(request.Notifications);
        }


        [HttpPost]
        [Route("enderecoEntrega")]
        public async Task<IActionResult> InserirEnderecoEntrega([FromBody] List<UsuarioEnderecoRequest> request)
        {
            return Ok(await _usuarioEnderecoEntregaService.SalvarAsync(request));
        }


        [HttpGet]   
        [Route("saldoPontos")]       
        public async Task<IActionResult> ConsultarSaldo(FiltroUsuario filtro)
        {
            return Ok(await _service.ObterSaldoDePontosAcumuladosPorUsuarioIdAsync(filtro));
        }


        [HttpGet]
        [Route("extrato")]
        public async Task<IActionResult> ConsultarExtrato(FiltroUsuario filtro)
        {
            return Ok(await _service.ObterExtratoPorUsuarioIdAsync(filtro));
        }
    }
}
