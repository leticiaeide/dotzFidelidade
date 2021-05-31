using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto;
using Dotz.Fidelidade.Domain.Dominio.Produto.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service;
using Dotz.Fidelidade.Domain.Dominio.Produto.Service;
using Dotz.Fidelidade.Domain.Interfaces;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dotz.Fidelidade.Tests.Services
{
    public class ProdutoResgateServiceTests
    {
        private readonly IProdutoResgateService _service;
        private readonly IProdutoResgateRepository _repository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoResgateServiceTests()
        {
            _repository = Substitute.For<IProdutoResgateRepository>();
            _pedidoRepository = Substitute.For<IPedidoRepository>();
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _service = new ProdutoResgateService( _repository, _produtoRepository, _pedidoRepository,_unitOfWork);
        }

        [Fact]
        public async Task Deve_Salvar_Resgate_Async()
        {
            var request = PreencherRequest();
            var produto = ObterProdutoResgate();
            var totalizador = ObterProdutoResgateTotalizador();
            var produtoMovimentacao = ObterProdutoResgateMovimentacao();

            _produtoRepository.ObterPorProdutoIdAsync(Arg.Any<int>()).Returns(produto);
            _repository.ObterResgateTotalizadorAsync(Arg.Any<int>()).Returns(totalizador);
            _repository.SalvarAsync(Arg.Any<ProdutoResgateMovimentacao>()).Returns(produtoMovimentacao);

            var retorno = _service.SalvarResgateAsync(request).Result;

            await _repository.Received().ObterResgateTotalizadorAsync(Arg.Any<int>());
            await _repository.Received().AtualizarResgateTotalizadorAsync(Arg.Any<ProdutoResgateTotalizador>());
          
            Assert.Equal(1, retorno.UsuarioId);
            Assert.Equal(1, retorno.ProdutoId);
            Assert.Equal(1000, retorno.QuantidadePontoUtilizado);          
        }

        [Fact]
        public async Task Deve_Atualizar_Resgate_Totalizador_Async()
        {
            var request = PreencherRequest();           
            var totalizador = ObterProdutoResgateTotalizador();        

            _repository.ObterResgateTotalizadorAsync(Arg.Any<int>()).Returns(totalizador);

            _repository.AtualizarResgateTotalizadorAsync(Arg.Any<ProdutoResgateTotalizador>()).Returns(true);

            var retorno = await _service.AtualizarResgateTotalizadorAsync(request.UsuarioId, request.PontosAcumuladosUtilizados);

            await _repository.Received().ObterResgateTotalizadorAsync(Arg.Any<int>());
            await _repository.Received().AtualizarResgateTotalizadorAsync(Arg.Any<ProdutoResgateTotalizador>());

            Assert.True(retorno);    
        }

        [Fact]
        public async Task Deve_exibir_Excessao_Ao_Verificar_Quantidade_Pontos_Produto_Insuficiente_Para_Resgate()
        {
            var request = PreencherRequest();
            request.PontosAcumuladosUtilizados = 100;

            var produto = ObterProdutoResgate();
            var totalizador = ObterProdutoResgateTotalizador();
            var produtoMovimentacao = ObterProdutoResgateMovimentacao();

            _produtoRepository.ObterPorProdutoIdAsync(Arg.Any<int>()).Returns(produto);
            _repository.ObterResgateTotalizadorAsync(Arg.Any<int>()).Returns(totalizador);
            _repository.SalvarAsync(Arg.Any<ProdutoResgateMovimentacao>()).Returns(produtoMovimentacao);

            var exceptionRetorno = Assert.ThrowsAsync<Exception>(() => _service.SalvarResgateAsync(request)).Result;

            await _repository.DidNotReceive().ObterResgateTotalizadorAsync(Arg.Any<int>());
            await _repository.DidNotReceive().AtualizarResgateTotalizadorAsync(Arg.Any<ProdutoResgateTotalizador>());

            Assert.Equal("A quantidade de pontos recebida é insuficiente para o resgaste do produto selecionado", exceptionRetorno.Message);         
        }

        [Fact]
        public async Task Deve_exibir_Excessao_Ao_Verificar_Quantidade_Pontos_Total_Insuficiente_Para_Resgate()
        {
            var request = PreencherRequest();    
            var produto = ObterProdutoResgate();
            var totalizador = ObterProdutoResgateTotalizador();
            totalizador.PontosAcumulados = 100;

            var produtoMovimentacao = ObterProdutoResgateMovimentacao();

            _produtoRepository.ObterPorProdutoIdAsync(Arg.Any<int>()).Returns(produto);
            _repository.ObterResgateTotalizadorAsync(Arg.Any<int>()).Returns(totalizador);
            _repository.SalvarAsync(Arg.Any<ProdutoResgateMovimentacao>()).Returns(produtoMovimentacao);

            var exceptionRetorno = Assert.ThrowsAsync<Exception>(() => _service.SalvarResgateAsync(request)).Result;       

            Assert.Equal("A quantidade total de pontos é insuficiente para o resgaste do produto selecionado", exceptionRetorno.Message);

            await _repository.Received().ObterResgateTotalizadorAsync(Arg.Any<int>());
            await _repository.DidNotReceive().AtualizarResgateTotalizadorAsync(Arg.Any<ProdutoResgateTotalizador>());
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

        private Produto ObterProdutoResgate()
        {
            return new Produto
            {
                Nome = "Televisão",
                QuantidadePontosPremiacao = 1000
            };
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

        private ProdutoResgateMovimentacao ObterProdutoResgateMovimentacao()
        {
            return new ProdutoResgateMovimentacao
            {
               DataResgate = DateTime.Now,
               ProdutoId = 1,
               QuantidadePontoUtilizado = 1000,
               UsuarioId = 1
            };
        }        
    }
}
