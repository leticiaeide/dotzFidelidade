using Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros;
using Dotz.Fidelidade.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterPorStatusAsync(FiltroPedido filtro);

        Task<IEnumerable<Pedido>> ObterPorUsuarioIdAsync(FiltroPedido filtro);
       
    }
}
