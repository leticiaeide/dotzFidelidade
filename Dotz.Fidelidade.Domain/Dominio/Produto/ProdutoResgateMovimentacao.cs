using Dotz.Fidelidade.Domain.Dominio.Produto.Enums;
using System;

namespace Dotz.Fidelidade.Domain.Dominio.Produto
{
    public class ProdutoResgateMovimentacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadePontoUtilizado { get; set; }
        public DateTime? DataResgate { get; set; }
        public DateTime? DataEntrada { get; set; }
        public TipoMovimentacao TipoMovimentacaoId { get; set; }

        public void ConverterDominio(int usuarioId, int produtoId, int quantidadePontoUtilizado, TipoMovimentacao tipoMovimentacaoId)
        {          
            UsuarioId = usuarioId;
            ProdutoId = produtoId;
            QuantidadePontoUtilizado = quantidadePontoUtilizado;
            DataResgate = PreencherDataEntrada(tipoMovimentacaoId);
            DataEntrada = PreencherDataSaida(tipoMovimentacaoId);
            TipoMovimentacaoId = tipoMovimentacaoId;
        }

        private DateTime? PreencherDataEntrada(TipoMovimentacao tipo)
        {
            if (tipo == TipoMovimentacao.Entrada)
            {
                return DateTime.Now;
            }
            return null;
        }

        private DateTime? PreencherDataSaida(TipoMovimentacao tipo)
        {
            if (tipo == TipoMovimentacao.Saida)
            {
                return DateTime.Now;
            }
            return null;
        }
    }
}
