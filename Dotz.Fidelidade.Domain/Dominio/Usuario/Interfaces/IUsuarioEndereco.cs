using Dotz.Fidelidade.Domain.Interfaces;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces
{
    public interface IUsuarioEnderecoRepository : IRepositoryBase<UsuarioEndereco>
    {
        Task<bool> AtualizarPorIdAsync(UsuarioEndereco entidade);
    }
}
