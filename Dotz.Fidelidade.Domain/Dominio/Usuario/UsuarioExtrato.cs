using Dotz.Fidelidade.Domain.Dominio.Produto;
using System.Collections.Generic;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario
{
    public class UsuarioExtrato
    {
        public UsuarioExtrato()
        {
            ResgatesMovimentacoes = new List<ProdutoResgateMovimentacaoExtrato>();
            ResgateTotalizador= new ProdutoResgateTotalizador();
        }

        public IEnumerable<ProdutoResgateMovimentacaoExtrato> ResgatesMovimentacoes { get; set; }
        public ProdutoResgateTotalizador ResgateTotalizador { get; set; }
        public int TotalQuantidadePontosUtilizados { get; set; }
    }
}
