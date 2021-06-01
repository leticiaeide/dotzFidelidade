using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Service;
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dotz.Fidelidade.Tests.Services
{
    public class AutenticacaoServiceTests
    {
        private readonly IAutenticacaoService _service;
        private readonly IUsuarioRepository _usuarioRepository;
       

        public AutenticacaoServiceTests()
        {
            _service = Substitute.For<IAutenticacaoService>();
            _usuarioRepository = Substitute.For<IUsuarioRepository>();
       
            _service = new AutenticacaoService(_usuarioRepository);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Perfil_Administrador()
        {
            var request = PreencherUsuarioRequest("admin@gerasenha.com.br", Domain.Dominio.Autenticacao.Enums.Perfil.Administrador, "geraUsuario");
                   
            var retorno = _service.GerarTokenAsync(request).Result;

            await _usuarioRepository.DidNotReceive().ObterPorEmailAsync(Arg.Any<string>());          

            Assert.NotNull(retorno);        
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Perfil_Usuario()
        {
            var request = PreencherUsuarioRequest("eide.leticia@gmail.com", Domain.Dominio.Autenticacao.Enums.Perfil.Usuario, "123456");
            var usuario = ObterUsuario();

            _usuarioRepository.ObterPorEmailAsync(Arg.Any<string>()).Returns(usuario);

            var retorno = _service.GerarTokenAsync(request).Result;

            await _usuarioRepository.Received().ObterPorEmailAsync(Arg.Any<string>());

            Assert.NotNull(retorno);
        }


        [Fact]
        public async Task Deve_Exibir_Excecao_Ao_Gerar_Token_Com_Perfil_Administrador()
        {
            var request = PreencherUsuarioRequest("eide.leticia@gmail.com", Domain.Dominio.Autenticacao.Enums.Perfil.Administrador, "123456");
            var usuario = ObterUsuario();

            _usuarioRepository.ObterPorEmailAsync(Arg.Any<string>()).Returns(usuario);
                        
            var exceptionRetorno = Assert.ThrowsAsync<Exception>(() => _service.GerarTokenAsync(request)).Result;

            Assert.Equal("Usuário administrador inválido", exceptionRetorno.Message);

            await _usuarioRepository.DidNotReceive().ObterPorEmailAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task Deve_Exibir_Excecao_Ao_Gerar_Token_Com_Perfil_Usuario_E_Usuario_Nao_Encontrado()
        {
            var request = PreencherUsuarioRequest("eide.leticia@gmail.com", Domain.Dominio.Autenticacao.Enums.Perfil.Usuario, "123456");
            User usuario = new User();
            usuario = null;

            _usuarioRepository.ObterPorEmailAsync(Arg.Any<string>()).Returns(usuario);

            var exceptionRetorno = Assert.ThrowsAsync<Exception>(() => _service.GerarTokenAsync(request)).Result;

            Assert.Equal("Usuário não encontrado para o e-mail", exceptionRetorno.Message);

            await _usuarioRepository.Received().ObterPorEmailAsync(Arg.Any<string>());
        }

     
        [Fact]
        public async Task Deve_Gerar_Token_Com_Perfil_Usuario_E_Senha_Invalida()
        {
            var request = PreencherUsuarioRequest("eide.leticia@gmail.com", Domain.Dominio.Autenticacao.Enums.Perfil.Usuario, "123456");
            User usuario = ObterUsuario();
            usuario.Senha = "1234567";

            _usuarioRepository.ObterPorEmailAsync(Arg.Any<string>()).Returns(usuario);

            var exceptionRetorno = Assert.ThrowsAsync<Exception>(() => _service.GerarTokenAsync(request)).Result;

            Assert.Equal("Senha usuário inválida", exceptionRetorno.Message);

            await _usuarioRepository.Received().ObterPorEmailAsync(Arg.Any<string>());
        }

        private UsuarioRequest PreencherUsuarioRequest(string email, Domain.Dominio.Autenticacao.Enums.Perfil perfil, string senha)
        {
            return new UsuarioRequest { Email = email, Perfil = perfil, Senha= senha };
        }

        private User ObterUsuario()
        {
            return new User { Email = "eide.leticia@gmail.com", Senha = "123456" };
        }
    }
}
