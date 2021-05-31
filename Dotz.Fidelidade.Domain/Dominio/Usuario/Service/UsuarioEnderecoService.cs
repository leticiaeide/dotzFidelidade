using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services;
using Dotz.Fidelidade.Domain.Interfaces;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Service
{
    public class UsuarioEnderecoService : ServiceBase, IUsuarioEnderecoService
    {
        private readonly IUsuarioEnderecoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioEnderecoService(IUsuarioEnderecoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }     

        public async Task<bool> SalvarAsync(IEnumerable<UsuarioEnderecoRequest> request)
        {
            //inserir em massa bulkinsermysql?           

            var usuarioEndereco = new UsuarioEndereco();
            var lista = new List<UsuarioEndereco>();

            foreach (var endereco in request)
                lista.Add(usuarioEndereco.ConverterDominio(endereco.Id, endereco.UsuarioId, endereco.Descricao, endereco.Numero, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.Cep, endereco.PadraoEntrega));

            if (!usuarioEndereco.PossuiEnderecoPadrao(lista))
                throw new Exception("Informe um endereço padrão");

            if (usuarioEndereco.PossuiMaisDeUmEnderecoPadrao(lista))
                throw new Exception("Informe apenas um unico endereço padrão");

            return await SalvarEndercos(lista);            
        }


        private async Task<bool> SalvarEndercos(List<UsuarioEndereco> lista)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                foreach (var endereco in lista)
                {
                    if (endereco.Id == 0)
                         await _repository.SalvarAsync(endereco);
                    else
                         await _repository.AtualizarPorIdAsync(endereco);
                }
                _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                return false;
            }

            return true;
        }
    }
}
