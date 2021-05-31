
using Dotz.Fidelidade.Domain.Dominio.Usuario;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dotz.Fidelidade.Domain.Dominio.Autenticacao.Service
{
    public class AutenticacaoJwtService
    {
        public static string GenerateToken(UsuarioToken user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Perfil.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(10),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static string PreencherFuncao(int roleNumber)
        {
            switch (roleNumber)
            {
                case 1:
                    return "Director";

                case 2:
                    return "Manager";

                case 3:
                    return "Colaborator";

                default:
                    throw new Exception();
            }
        }
    }
}
