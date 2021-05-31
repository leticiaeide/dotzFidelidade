using Dotz.Fidelidade.Domain.Dominio.Produto.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service
{
    public interface IProdutoResgateService 
    {
        Task<ProdutoResgateMovimentacao> SalvarResgateAsync(ProdutoResgateMovimentacaoRequest request);        

        Task<ProdutoResgateTotalizador> ObterResgateTotalizadorAsync(FiltroProduto filtro);

        Task<bool> AtualizarResgateTotalizadorAsync(int usuarioId, int pontosAcumuladosUtilizados);
    }
}
