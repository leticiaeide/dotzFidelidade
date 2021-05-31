using Dotz.Fidelidade.Domain.Interfaces;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Filtros
{
    public class FiltroProduto :  RequestBase, IRequest
    {       
        public int QuantidadePontosPremiacao { get; set; }    
        public int UsuarioId { get; set; }

        public void Validate()
        {
            if (UsuarioId == 0)
            {
                AddNotification("UsuarioId", "UsuarioId obrigatório");
            }

            if (QuantidadePontosPremiacao == 0)
            {
                AddNotification("QuantidadePontosPremiacao", "QuantidadePontosPremiacao deve ser maior que zero");
            }
        }
    }
}
