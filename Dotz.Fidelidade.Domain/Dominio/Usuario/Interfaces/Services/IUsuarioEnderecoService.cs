using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services
{
    public interface IUsuarioEnderecoService
    {
        Task<bool> SalvarAsync(IEnumerable<UsuarioEnderecoRequest> request);    
    }
}
