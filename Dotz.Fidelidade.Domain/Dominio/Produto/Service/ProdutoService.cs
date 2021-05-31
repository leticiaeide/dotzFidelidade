using Dotz.Fidelidade.Domain.Dominio.Produto.Filtros;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces;
using Dotz.Fidelidade.Domain.Dominio.Produto.Interfaces.Service;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Service
{
    public class ProdutoService : ServiceBase, IProdutoService
    {
        private readonly IProdutoRepository _repository;
    
        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }       

        public async Task<IEnumerable<Produto>> ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(FiltroProduto filtro)
        {
            return await _repository.ObterDisponiveisParaResgatePorQuantidadePontoPremiacaoAsync(filtro);
        }
    }
}
