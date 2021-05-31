using Dotz.Fidelidade.Domain.Dominio.Produto;
using Dotz.Fidelidade.Domain.Dominio.Produto.Arguments;
using System;
using Xunit;

namespace Dotz.Fidelidade.Tests.Entities
{
    public class ProdutoResgateTotalizadorTests
    {
        [Fact]
        public void Deve_Calcular_Pontos_Restantes_Acumulados()
        {
            var request = PreencherRequest();
            var totalizador = ObterProdutoResgateTotalizador();
         
            var retorno = totalizador.CalcularPontosRestantesAcumulados(totalizador.PontosAcumulados, request.PontosAcumuladosUtilizados);

            Assert.Equal(500, retorno);
            
        }

        private ProdutoResgateTotalizador ObterProdutoResgateTotalizador()
        {
            return new ProdutoResgateTotalizador
            {
                DataUltimaAtualizacao = new DateTime(2021, 04, 01),
                PontosAcumulados = 2000,
                UsuarioId = 1
            };
        }

        private ProdutoResgateMovimentacaoRequest PreencherRequest()
        {
            return new ProdutoResgateMovimentacaoRequest
            {
                UsuarioId = 1,
                ProdutoId = 1,
                PontosAcumuladosUtilizados = 1500
            };
        }
    }
}
