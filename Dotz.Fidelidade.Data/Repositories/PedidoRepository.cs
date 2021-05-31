using Dapper;
using Dotz.Fidelidade.Domain.Dominio.Pedido;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private DbSession _session;

        public PedidoRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Pedido>> ObterPorStatusAsync(FiltroPedido filtro)
        {
            return await _session.Connection.QueryAsync<Pedido>("SELECT * FROM Pedido WHERE UsuarioId = @usuarioId AND StatusEntrega = @statusEntrega",
                                                       new
                                                       {
                                                           filtro.UsuarioId,
                                                           filtro.StatusEntrega
                                                       }, transaction: _session.Transaction);
        }

        public async Task<IEnumerable<Pedido>> ObterPorUsuarioIdAsync(FiltroPedido filtro)
        {
            return await _session.Connection.QueryAsync<Pedido>("SELECT * FROM Pedido WHERE UsuarioId = @usuarioId",
                                                        new
                                                        {
                                                            filtro.UsuarioId                                                         
                                                        }, transaction: _session.Transaction);
        }

        public async Task<Pedido> SalvarAsync(Pedido entidade)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<Pedido>("INSERT INTO Pedido VALUES (@id, @usuarioId, @statusEntrega)",
                new
                {
                    entidade.Id,
                    entidade.UsuarioId,                    
                    entidade.StatusEntrega,                                    
                }, transaction: _session.Transaction);
        }
    }
}
