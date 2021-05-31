namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments
{
    public class UsuarioEnderecoRequest : RequestBase
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public bool PadraoEntrega { get; set; }

        //validar ainda não esquecer
        public void Validate()
        {
            if (UsuarioId == 0)
            {
                AddNotification("IdUsuario", "IdUsuario obrigatório");
            }

            if (string.IsNullOrEmpty(Descricao))
            {
                AddNotification("Descricao", "Descricao obrigatório");
            }

            if (string.IsNullOrEmpty(Numero))
            {
                AddNotification("Numero", "Numero obrigatório");
            }

            if (string.IsNullOrEmpty(Bairro))
            {
                AddNotification("Bairro", "Bairro obrigatório");
            }

            if (string.IsNullOrEmpty(Cidade))
            {
                AddNotification("Cidade", "Cidade obrigatório");
            }

            if (string.IsNullOrEmpty(Estado))
            {
                AddNotification("Estado", "Estado obrigatório");
            }

            if (string.IsNullOrEmpty(Cep))
            {
                AddNotification("Cep", "Cidade obrigatório");
            }
        }
    }
}
