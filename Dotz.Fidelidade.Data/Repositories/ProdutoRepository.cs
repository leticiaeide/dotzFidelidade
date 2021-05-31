using Dapper;
using Dotz.Fidelidade.Domain.Dominio.Produto;
using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {       
        private DbSession _session;

        public ProdutoRepository(DbSession session)
        {
            _session = session;
        }
        
        public async Task<IEnumerable<Produto>> ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(FiltroProduto filtro)
        {
            return await _session.Connection.QueryAsync<Produto>("SELECT * FROM Produto WHERE QuantidadePontosPremiacao <= @quantidadePontosPremiacao", new
            {
                filtro.QuantidadePontosPremiacao
            }, transaction: _session.Transaction);
        }

        public async Task<Produto> ObterPorProdutoIdAsync(int id)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE Id = @id", new
            {
                id
            }, transaction: _session.Transaction);
        }

        public async Task<Produto> SalvarAsync(Produto entidade)
        {
            throw new System.NotImplementedException();
        }      
    }
}
