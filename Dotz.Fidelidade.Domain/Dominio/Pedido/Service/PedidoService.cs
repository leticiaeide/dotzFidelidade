using Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido.Service
{
    public class PedidoService : ServiceBase, IPedidoService
    {
        private readonly IPedidoRepository _repository; 
        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;         
        }

        public async Task<IEnumerable<Pedido>> ObterPorStatusAsync(FiltroPedido filtro)
        {
            return await _repository.ObterPorStatusAsync(filtro);
        }

        public async Task<IEnumerable<Pedido>> ObterPorUsuarioIdAsync(FiltroPedido filtro)
        {
            return await _repository.ObterPorUsuarioIdAsync(filtro);
        }        
    }
}
