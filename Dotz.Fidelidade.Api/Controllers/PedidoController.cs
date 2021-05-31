using Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Api.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;
        public PedidoController(IPedidoService service) 
        {
            _service = service;
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> ObterPedidosPorStatus(FiltroPedido filtro)
        {
            return Ok(await _service.ObterPorStatusAsync(filtro));
        }

        [HttpGet]
        [Route("usuario")]
        public async Task<IActionResult> ObterPedidosPorUsuario(FiltroPedido filtro)
        {
            return Ok(await _service.ObterPorUsuarioIdAsync(filtro));
        }
    }
}
