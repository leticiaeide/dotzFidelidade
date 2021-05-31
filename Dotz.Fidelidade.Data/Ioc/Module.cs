using Dotz.Fidelidade.Data.Repositories;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using Dotz.Fidelidade.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Dotz.Fidelidade.Data.Ioc
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var references = new Dictionary<Type, Type>
            {
                { typeof(IUsuarioRepository), typeof(UsuarioRepository) },
                { typeof(IUsuarioEnderecoRepository), typeof(UsuarioEnderecoRepository) },
                { typeof(IProdutoRepository), typeof(ProdutoRepository) },
                { typeof(IProdutoResgateRepository), typeof(ProdutoResgateRepository) },
                { typeof(IPedidoRepository), typeof(PedidoRepository) },
                { typeof(IUnitOfWork), typeof(UnitOfWork) }
            };
            return references;
        }
    }
}
