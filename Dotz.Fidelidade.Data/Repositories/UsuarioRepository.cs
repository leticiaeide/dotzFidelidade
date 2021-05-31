using Dapper;
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private DbSession _session;

        public UsuarioRepository(DbSession session)
        {
            _session = session;
        }      

        public async Task<User> SalvarAsync(User entidade)
        {
            await _session.Connection.ExecuteScalarAsync("INSERT INTO Usuario VALUES (@id, @email, @senha)",
                                                                new
                                                                {
                                                                    entidade.Id,
                                                                    entidade.Email,
                                                                    entidade.Senha
                                                                }, transaction: _session.Transaction);
            return entidade;
        }


        public async Task<User> ObterPorEmailAsync(string email)
        {
            return await _session.Connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Usuario WHERE Email = @email", new
            {
               email
            }, transaction: _session.Transaction);
        }
    }
}
