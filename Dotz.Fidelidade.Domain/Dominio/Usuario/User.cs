using System.Text.RegularExpressions;

namespace Dotz.Fidelidade.Domain.Dominio.Usuario
{
    public class User
    {
        public int Id { get; set;}       
        public string Email { get; set; }
        public string Senha { get; set; }

        public void ConverterDominio(string email, string senha)
        {           
            Email = email;
            Senha = senha;
        }

        public bool EmailValido(string email)
        {
            Regex rg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return rg.IsMatch(email);  
        }       
    }
}
