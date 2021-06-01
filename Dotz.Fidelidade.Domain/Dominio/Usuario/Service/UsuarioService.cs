using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services;
using Dotz.Fidelidade.Domain.Interfaces;
using Dotz.Fidelidade.Domain.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Service
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {

        private readonly IUsuarioRepository _repository;
        private readonly IProdutoResgateRepository _produtoResgateRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioService(IUsuarioRepository repository,
                            IProdutoResgateRepository produtoResgateRepository,
                            IUnitOfWork unitOfWork) 
        {
            _repository = repository;
            _produtoResgateRepository = produtoResgateRepository;
            _unitOfWork = unitOfWork;
        }       
        public async Task<int> ObterSaldoDePontosAcumuladosPorUsuarioIdAsync(FiltroUsuario filtro)
        {
            var resgate = await _produtoResgateRepository.ObterResgateTotalizadorAsync(filtro.UsuarioId);

            if (resgate == null)            
                 throw new Exception ("Saldo não encontrado para o usuário");            

            return resgate.PontosAcumulados;
        }

        public async Task<User> SalvarAsync(UsuarioRequest request)
        {
            _unitOfWork.BeginTransaction();
           
            var usuario = new User();

            if (!usuario.EmailValido(request.Email))
                throw new Exception("Formato E-mail Inválido"); 

            usuario.ConverterDominio(request.Email, GeneratorMd5.GerarHashMd5(request.Senha));

            if (UsuarioJaExiste(usuario))
                throw new Exception("E-mail já cadastrado");         

            var retorno = await _repository.SalvarAsync(usuario);

            _unitOfWork.Commit();

            return retorno;
        }

        public async Task<UsuarioExtrato> ObterExtratoPorUsuarioIdAsync(FiltroUsuario filtro)
        {
            var retorno = new UsuarioExtrato
            {
            };

            retorno.ResgateTotalizador = await _produtoResgateRepository.ObterResgateTotalizadorAsync(filtro.UsuarioId);
            retorno.ResgatesMovimentacoes = await _produtoResgateRepository.ObterResgateMovimentacaoAsync(filtro.UsuarioId);
            retorno.TotalQuantidadePontosUtilizados = retorno.ResgatesMovimentacoes.ToList().Sum(m => m.QuantidadePontoUtilizado);          

            return retorno;
        }

        public async Task<User> ObterPorEmailAsync(UsuarioRequest request)
        {
            var usuario = new User();
            usuario.ConverterDominio(request.Email, request.Senha);

            return await _repository.ObterPorEmailAsync(usuario.Email);
        }

        private bool UsuarioJaExiste(User usuario)
        {          
            var usuarioExiste =  _repository.ObterPorEmailAsync(usuario.Email).Result;

            if (usuarioExiste == null)            
                return false;           

            return true;          
        }
    }
}
