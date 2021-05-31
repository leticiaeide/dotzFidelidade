using Dotz.Fidelidade.Domain.Interfaces;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<User>
    {
        Task<User> ObterPorEmailAsync(string email);
    }
}
