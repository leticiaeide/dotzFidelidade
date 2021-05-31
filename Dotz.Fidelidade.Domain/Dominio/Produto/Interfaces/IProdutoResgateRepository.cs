using Dotz.Fidelidade.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces
{
    public interface IProdutoResgateRepository : IRepositoryBase<ProdutoResgateMovimentacao>
    {
        Task<bool> AtualizarResgateTotalizadorAsync(ProdutoResgateTotalizador entidade);
        Task<ProdutoResgateTotalizador> ObterResgateTotalizadorAsync(int usuarioId);
        Task<IEnumerable<ProdutoResgateMovimentacaoExtrato>> ObterResgateMovimentacaoAsync(int usuarioId);
    }
}
