using Dotz.Fidelidade.Domain.Dominio.Pedido.Enums;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }        
        public StatusEntrega StatusEntrega { get; set; }

        public void ConverterDominio(int id, int usuarioId, StatusEntrega statusEntrega)
        {
            Id = id;
            UsuarioId = usuarioId;            
            StatusEntrega = statusEntrega;
        }      
    }
}
