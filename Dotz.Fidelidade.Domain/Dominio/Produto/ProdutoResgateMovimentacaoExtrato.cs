using Dotz.Fidelidade.Domain.Dominio.Produto.Enums;
using System;

namespace Dotz.Fidelidade.Domain.Dominio.Produto
{
    public class ProdutoResgateMovimentacaoExtrato
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadePontoUtilizado { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataEntrada { get; set; }
        public TipoMovimentacao TipoMovimentacaoId { get; set; }
    }
}

