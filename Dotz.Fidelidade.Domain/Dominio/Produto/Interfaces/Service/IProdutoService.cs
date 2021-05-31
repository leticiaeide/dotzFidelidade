using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service
{
    public interface IProdutoService 
    {
        Task<IEnumerable<Produto>> ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(FiltroProduto filtro);
    }
}
