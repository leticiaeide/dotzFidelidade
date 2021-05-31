using Dotz.Fidelidade.CrossCutting;
using Dotz.Fidelidade.Data.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace Dotz.Fidelidade.Api
{
    public class IoCConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbSession>();           

            Bootstraper.RegisterService(services, configuration);
        }
    }
}
