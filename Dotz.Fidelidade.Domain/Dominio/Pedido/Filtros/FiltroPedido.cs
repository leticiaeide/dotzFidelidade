using Dotz.Fidelidade.Domain.Dominio.Pedido.Enums;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido.Filtros
{
    public class FiltroPedido
    {
        public int UsuarioId { get; set; }

        public StatusEntrega StatusEntrega { get; set; }
    }
}
