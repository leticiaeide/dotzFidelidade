using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Autenticacao.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string> GerarTokenAsync(UsuarioRequest request);
    }
}
