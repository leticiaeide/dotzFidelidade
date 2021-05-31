using Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces.Service
{
    public interface IPedidoService 
    {
        Task<IEnumerable<Pedido>> ObterPorStatusAsync(FiltroPedido filtro);

        Task<IEnumerable<Pedido>> ObterPorUsuarioIdAsync(FiltroPedido filtro);
    }
}
