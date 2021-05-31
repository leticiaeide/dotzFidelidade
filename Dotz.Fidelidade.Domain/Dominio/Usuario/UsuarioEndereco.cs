using System.Collections.Generic;
using System.Linq;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario
{
    public class UsuarioEndereco
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public bool PadraoEntrega { get; set; }

        public UsuarioEndereco ConverterDominio(int id, int idUsuario, string descricao, string numero, string bairro, string cidade, string estado, string cep, bool padraoEntrega)
        {
            return new UsuarioEndereco
            {
                Id = id,
                IdUsuario = idUsuario,
                Descricao = descricao,
                Numero = numero,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Cep = cep,
                PadraoEntrega = padraoEntrega
            };
        }

        public bool PossuiMaisDeUmEnderecoPadrao(IEnumerable<UsuarioEndereco> lista)
        {
            return lista.Where(e => e.PadraoEntrega == true).ToList().Count > 1;
        }

        public bool PossuiEnderecoPadrao(IEnumerable<UsuarioEndereco> lista)
        {
            return lista.Where(e => e.PadraoEntrega == true).ToList().Any();    
        }
    }
}
