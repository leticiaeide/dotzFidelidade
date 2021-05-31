using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Enums;
using Dotz.Fidelidade.Domain.Interfaces;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario.Arguments
{
    public class UsuarioRequest : RequestBase, IRequest
    {      
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                AddNotification("Email", "Email obrigatório");
            }

            if (string.IsNullOrEmpty(Senha))
            {
                AddNotification("Senha", "Senha obrigatório");
            }   
        }
    }
}
