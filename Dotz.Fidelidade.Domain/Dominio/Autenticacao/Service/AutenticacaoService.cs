using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using Dotz.Fidelidade.Domain.Util;
using System;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Autenticacao.Service
{
    public class AutenticacaoService : ServiceBase, IAutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
      
        public AutenticacaoService(IUsuarioRepository repository)
        {
            _repository = repository;        
        }

        public async Task<string> GerarTokenAsync(UsuarioRequest request)
        {
            var usuarioToken = new UsuarioToken();
            usuarioToken.ConverterDominio(request.Email, request.Senha, request.Perfil);

            if (usuarioToken.PerfilAdministrador())
            {
                return await GerarTokenPerfilAdministrador(request,usuarioToken);
            }
            else
            {
                return await GerarTokenPerfilUsuario(request, usuarioToken);
            }    
        }

        private bool DadosAutenticacaoAdministradorValidos(UsuarioToken usuarioToken)
        {              
            return usuarioToken.Email == "admin@gerasenha.com.br" && usuarioToken.Senha == "geraUsuario"; 
        }

        private bool SenhaValida(string senha, string senhaToken)
        {           
            return GeneratorMd5.GerarHashMd5(senha) == GeneratorMd5.GerarHashMd5(senhaToken);
        }

        private async Task<string> GerarTokenPerfilUsuario(UsuarioRequest request, UsuarioToken usuarioToken)
        {
            var user = await _repository.ObterPorEmailAsync(request.Email);

            if (user == null && request.Perfil == Enums.Perfil.Usuario)
                throw new Exception("Usuário não encontrado para o e-mail");

            if (!SenhaValida(user.Senha, request.Senha))
                throw new Exception("Senha usuário inválida");

            return AutenticacaoJwtService.GenerateToken(usuarioToken);
        }


        private async Task<string> GerarTokenPerfilAdministrador(UsuarioRequest request, UsuarioToken usuarioToken)
        {
            if (DadosAutenticacaoAdministradorValidos(usuarioToken))
            {
                return AutenticacaoJwtService.GenerateToken(usuarioToken);
            }
            else
            {
                throw new Exception("Usuário administrador inválido");
            }
        }
    }
}
