using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Service;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Interfaces.Service;
using Dotz.Fidelidade.Domain.Dominio.Pedido.Service;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service;
using Dotz.Fidelidade.Domain.Dominio.Produto.Service;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Service;
using System;
using System.Collections.Generic;

namespace Dotz.Fidelidade.Domain.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var result = new Dictionary<Type, Type>
            {
                { typeof(IUsuarioService), typeof(UsuarioService) },
                { typeof(IUsuarioEnderecoService), typeof(UsuarioEnderecoService) },
                { typeof(IPedidoService), typeof(PedidoService) },
                { typeof(IProdutoService), typeof(ProdutoService) },
                { typeof(IProdutoResgateService), typeof(ProdutoResgateService) },
                { typeof(IAutenticacaoService), typeof(AutenticacaoService) }
            };
            return result;
        }
    }
}
