using Dotz.Fidelidade.Domain.Dominio.Produto.Enums;
using Dotz.Fidelidade.Domain.Interfaces;
using System;

namespace Dotz.Fidelidade.Domain.Dominio.Produto.Arguments
{
    public class ProdutoResgateMovimentacaoRequest : RequestBase, IRequest
    {    
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public int PontosAcumuladosUtilizados { get; set; }      
        public TipoMovimentacao TipoMovimentacaoId { get; set; }

        public void Validate()
        {
            if (UsuarioId == 0)
            {
                AddNotification("UsuarioId", "UsuarioId obrigatório");
            }

            if (ProdutoId == 0)
            {
                AddNotification("ProdutoId", "ProdutoId obrigatório");
            }

            if (PontosAcumuladosUtilizados == 0)
            {
                AddNotification("QuantidadePontoPremiacaoUtilizado", "QuantidadePontoPremiacaoUtilizado obrigatório");
            }           
        }
    }
}
