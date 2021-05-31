using Dotz.Fidelidade.Domain.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dotz.Fidelidade.CrossCutting
{
    public static class Bootstraper
    {
        public static void RegisterService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.RegisterTypes(Module.GetTypes());

            services.RegisterTypes(Data.Ioc.Module.GetTypes());
        }

        public static void RegisterTypes(this IServiceCollection container, Dictionary<Type, Type> types)
        {
            foreach (var item in types)
            {
                container.AddScoped(item.Key, item.Value);
            }
        }
    }
}