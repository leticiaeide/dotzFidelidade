using Dotz.Fidelidade.Domain.Dominio.Produto.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Api.Controllers
{
    [Authorize(Roles = "Usuario")]
    [Route("api/[controller]")]    
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;
        private readonly IProdutoResgateService _produtoResgateService;
        public ProdutoController(IProdutoService service, IProdutoResgateService produtoResgateService) 
        {
            _service = service;
            _produtoResgateService = produtoResgateService;
        }

        [HttpGet]
        [Route("disponivelResgate")]
        public async Task<IActionResult> ConsultarDisponiveisResgate(FiltroProduto filtro)
        {
            filtro.Validate();

            if (filtro.Valid)
            {
                return Ok(await _service.ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(filtro));
            }
            return BadRequest(filtro.Notifications);
        }

        [HttpPost]
        [Route("resgate")]
        public async Task<IActionResult> EfetuarResgate([FromBody] ProdutoResgateMovimentacaoRequest request)
        {
            request.Validate();

            if (request.Valid)
            {
                return Ok(await _produtoResgateService.SalvarResgateAsync(request));
            }
            return BadRequest(request.Notifications);
        }
    }
}
