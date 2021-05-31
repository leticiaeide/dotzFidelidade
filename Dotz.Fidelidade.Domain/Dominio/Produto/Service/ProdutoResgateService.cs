using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service;
using Dotz.Fidelidade.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Service
{
    public class ProdutoResgateService: ServiceBase, IProdutoResgateService
    {
        private readonly IProdutoResgateRepository _repository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;
 

        public ProdutoResgateService(IProdutoResgateRepository repository, IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }               

        public async Task<ProdutoResgateMovimentacao> SalvarResgateAsync(ProdutoResgateMovimentacaoRequest request)
        {
            _unitOfWork.BeginTransaction();
            var retorno = new ProdutoResgateMovimentacao();

            try
            {
                var produtoResgateMovimentacao = new ProdutoResgateMovimentacao();
                produtoResgateMovimentacao.ConverterDominio(request.UsuarioId, request.ProdutoId, request.PontosAcumuladosUtilizados, request.TipoMovimentacaoId);

                var produto = new Produto();

                if (!QuantidadePontosProdutoSuficientesParaResgate(request.ProdutoId, request.PontosAcumuladosUtilizados))
                    throw new Exception("A quantidade de pontos recebida é insuficiente para o resgaste do produto selecionado");                

                if (!QuantidadePontosTotalSuficientesParaResgate(request.ProdutoId, request.PontosAcumuladosUtilizados))
                    throw new Exception("A quantidade total de pontos é insuficiente para o resgaste do produto selecionado");              

                retorno = await _repository.SalvarAsync(produtoResgateMovimentacao);

                await AtualizarResgateTotalizadorAsync(request.UsuarioId, request.PontosAcumuladosUtilizados);

                await InserirPedido(request.UsuarioId);

                _unitOfWork.Commit();           

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
            return retorno;
        }

        public async Task<ProdutoResgateTotalizador> ObterResgateTotalizadorAsync(FiltroProduto filtro)
        {
            return await _repository.ObterResgateTotalizadorAsync(filtro.UsuarioId);
        }              
      
        public async Task<bool> AtualizarResgateTotalizadorAsync(int usuarioId, int pontosAcumuladosUtilizados)
        {
            var produtoResgateTotalizador = new ProdutoResgateTotalizador();

            var totalizadorExistente = _repository.ObterResgateTotalizadorAsync(usuarioId).Result;
            
            int restantePontosAcumulados = produtoResgateTotalizador.CalcularPontosRestantesAcumulados(totalizadorExistente.PontosAcumulados, pontosAcumuladosUtilizados);

            produtoResgateTotalizador.ConverterDominio(totalizadorExistente.Id, usuarioId, restantePontosAcumulados);

            return await _repository.AtualizarResgateTotalizadorAsync(produtoResgateTotalizador);
        }

        private bool QuantidadePontosProdutoSuficientesParaResgate(int produtoId, int quantidadePontosPremiacao)
        {
            var produto = _produtoRepository.ObterPorProdutoIdAsync(produtoId).Result;

            if (produto == null)
                return false;

            return quantidadePontosPremiacao >= produto.QuantidadePontosPremiacao;
        }

        private bool QuantidadePontosTotalSuficientesParaResgate(int usuarioId, int quantidadePontosPremiacao)
        {
            var totalPontosAcumuladosAtual = _repository.ObterResgateTotalizadorAsync(usuarioId).Result;

            return quantidadePontosPremiacao < totalPontosAcumuladosAtual.PontosAcumulados;
        }

        private async Task InserirPedido(int usuarioId)
        {
            var pedido = new Pedido.Pedido();
            pedido.ConverterDominio(0, usuarioId, Pedido.Enums.StatusEntrega.Pendente);
            await _pedidoRepository.SalvarAsync(pedido);
        }
    }
}
