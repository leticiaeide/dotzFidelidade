using Dapper;
using Dotz.Fidelidade.Domain.Dominio.Produto;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Data.Repositories
{
    public class ProdutoResgateRepository: IProdutoResgateRepository
    {     
        private DbSession _session;

        public ProdutoResgateRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<bool> AtualizarResgateTotalizadorAsync(ProdutoResgateTotalizador entidade)
        {
            await _session.Connection.QueryAsync<bool>("UPDATE ProdutoResgateTotalizador SET PontosAcumulados = @pontosAcumulados, DataUltimaAtualizacao = @dataUltimaAtualizacao WHERE Id = @id",
                                                          new
                                                          {
                                                              entidade.Id,                                                          
                                                              entidade.PontosAcumulados,
                                                              entidade.DataUltimaAtualizacao
                                                          }, transaction: _session.Transaction);

            return true;
        }

        public async Task<ProdutoResgateTotalizador> ObterResgateTotalizadorAsync(int usuarioId)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<ProdutoResgateTotalizador>("SELECT * FROM ProdutoResgateTotalizador WHERE UsuarioId = @usuarioId",
                                                        new
                                                        {
                                                            usuarioId
                                                        }, transaction: _session.Transaction);
        }

        public async Task<ProdutoResgateMovimentacao> SalvarAsync(ProdutoResgateMovimentacao entidade)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<ProdutoResgateMovimentacao>("INSERT INTO ProdutoResgateMovimentacao VALUES (@id, @usuarioId, @produtoId, @quantidadePontoUtilizado, @dataResgate)",
                                                         new
                                                         {
                                                             entidade.Id,
                                                             entidade.UsuarioId,
                                                             entidade.ProdutoId,
                                                             entidade.QuantidadePontoUtilizado,
                                                             entidade.DataResgate
                                                         }, transaction: _session.Transaction);
        }

        public async Task<IEnumerable<ProdutoResgateMovimentacaoExtrato>> ObterResgateMovimentacaoAsync(int usuarioId)
        {
            return await _session.Connection.QueryAsync<ProdutoResgateMovimentacaoExtrato>(" SELECT PRM.Id, PRM.UsuarioId, PRM.ProdutoId, PRM.QuantidadePontoUtilizado,  PRM.DataResgate, " +
                                                                                           " P.Nome AS NomeProduto, PRM.DataEntrada, PRM.TipoMovimentacaoId " +
                                                                                           " FROM ProdutoResgateMovimentacao as PRM INNER JOIN Produto AS P ON P.Id = PRM.ID " +
                                                                                           " WHERE UsuarioId = @usuarioId", new
                                                                                           {
                                                                                               usuarioId
                                                                                           }, transaction: _session.Transaction);
        }
    }
}
