using System;

namespace Dotz.Fidelidade.Domain.Dominio.Produto
{
    public class ProdutoResgateTotalizador
    {  
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PontosAcumulados { get; set; }      
        public DateTime DataUltimaAtualizacao { get; set; }

        public void ConverterDominio(int id, int usuarioId, int pontosAcumulados)
        {
            Id = id;
            UsuarioId = usuarioId;
            PontosAcumulados = pontosAcumulados;
            DataUltimaAtualizacao = DateTime.Now;
        }

        public int CalcularPontosRestantesAcumulados(int pontoAcumulados, int pontosAcumuladosUtilizados)
        {
            return pontoAcumulados - pontosAcumuladosUtilizados;
        }
    }
}
