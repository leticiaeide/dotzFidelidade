using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Dotz.Fidelidade.Data.Repositories
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        protected readonly IConfiguration _configuracao;

        public DbSession(IConfiguration configuration)
        {
            _configuracao = configuration;
            _id = Guid.NewGuid();
            Connection = new MySqlConnection(_configuracao.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }
        
        public MySqlConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }    
        public void Dispose() => Connection?.Dispose();
    }
}
