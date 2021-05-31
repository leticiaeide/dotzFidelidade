using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> SalvarAsync(T entidade);        
    }
}
