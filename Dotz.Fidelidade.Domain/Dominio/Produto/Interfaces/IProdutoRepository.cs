using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using Dotz.Fidelidade.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<IEnumerable<Produto>> ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(FiltroProduto filtro);

        Task<Produto> ObterPorProdutoIdAsync(int produtoId);
    }
}
