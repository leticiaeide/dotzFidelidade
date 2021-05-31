using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
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

            if (usuarioToken.PerfilAdministrador() && !DadosAutenticacaoAdministradorValidos(usuarioToken))
                throw new Exception("Usuário Administrador inválido");

            var usuarioExiste = await _repository.ObterPorEmailAsync(request.Email);

            if (usuarioExiste == null)
                throw new Exception("Usuário não encontrado para o e-mail");

            if (!SenhaValida(usuarioExiste.Senha, request.Senha))
                throw new Exception("Senha usuário inválida");

            return AutenticacaoJwtService.GenerateToken(usuarioToken);
        }

        private bool DadosAutenticacaoAdministradorValidos(UsuarioToken usuarioToken)
        {              
            return usuarioToken.Email == "admin@gerasenha.com.br" && usuarioToken.Senha == "geraUsuario"; 
        }

        private bool SenhaValida(string senha, string senhaToken)
        {
            return senha == senhaToken;
        }
    }
}
