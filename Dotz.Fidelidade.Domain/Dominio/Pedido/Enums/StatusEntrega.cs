using System;

namespace Dotz.Fidelidade.Domain.Dominio.Pedido.Enums
{
    [Flags]
    public enum StatusEntrega
    {
        Pendente = 1,
        Enviado = 2,
        Entregue = 3
    }
}
