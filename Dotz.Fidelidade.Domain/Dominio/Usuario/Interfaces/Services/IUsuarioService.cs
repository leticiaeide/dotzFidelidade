using Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments;
using Dotz.Fidelidade.Domain.Dominio.Usuario.Filtros;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<User> SalvarAsync(UsuarioRequest request);

        Task<int> ObterSaldoDePontosAcumuladosPorUsuarioIdAsync(FiltroUsuario filtro);

        Task<UsuarioExtrato> ObterExtratoPorUsuarioIdAsync(FiltroUsuario filtro);

        Task<User> ObterPorEmailAsync(UsuarioRequest request);     
    }
}
