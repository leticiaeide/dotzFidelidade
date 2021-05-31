using Dotz.Fidelidade.Domain.Dominio.Autenticacao.Enums;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario
{
    public class UsuarioToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        public void ConverterDominio(string email, string senha, Perfil perfil)
        {
            Email = email;
            Senha = senha;
            Perfil = perfil;
        }
        public bool PerfilAdministrador()
        {
            return Perfil == Perfil.Administrador;
        }
    }
}
