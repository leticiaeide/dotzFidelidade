using Dapper;
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Data.Repositories
{
    public class UsuarioEnderecoRepository : IUsuarioEnderecoRepository
    {
        private DbSession _session;

        public UsuarioEnderecoRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<UsuarioEndereco> SalvarAsync(UsuarioEndereco entidade)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<UsuarioEndereco>("INSERT INTO UsuarioEndereco VALUES (@id, @idUsuario, @descricao, @numero, @bairro, @cidade, @estado, @cep, @padraoEntrega)",
                                                                new
                                                                {
                                                                    entidade.Id,
                                                                    entidade.IdUsuario,
                                                                    entidade.Descricao,
                                                                    entidade.Numero,
                                                                    entidade.Bairro,
                                                                    entidade.Cidade,
                                                                    entidade.Estado,
                                                                    entidade.Cep,
                                                                    entidade.PadraoEntrega
                                                                }, transaction: _session.Transaction);
        }

        public async Task<bool> AtualizarPorIdAsync(UsuarioEndereco entidade)
        {
            await _session.Connection.QueryAsync<bool>("UPDATE UsuarioEndereco SET Descricao = @descricao, Numero = @numero, Bairro = @bairro, Cidade = @cidade, Estado = @estado," +
                                                       " Cep = @cep, PadraoEntrega = @padraoEntrega  WHERE Id = @id",
                                                         new
                                                         {
                                                             entidade.Id,
                                                             entidade.Descricao,
                                                             entidade.Numero,
                                                             entidade.Bairro,
                                                             entidade.Cidade,
                                                             entidade.Estado,
                                                             entidade.Cep,
                                                             entidade.PadraoEntrega
                                                         }, transaction: _session.Transaction);

            return true;
        }
    }
}
