using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VigneCommerce.Application.Dto;
using VigneCommerce.Application.Interfaces;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Application.Services
{
    public class TokenAppService : ITokenAppService
    {
        public LoginTokenDto GerarToken(Usuario usuario, string chaveToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(chaveToken);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var dataExpiracao = DateTime.UtcNow.AddHours(2);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GerarClaims(usuario),
                Expires = dataExpiracao,
                SigningCredentials = credentials,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return new LoginTokenDto(handler.WriteToken(token), dataExpiracao);
        }

        private static ClaimsIdentity GerarClaims(Usuario usuario)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, usuario.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Role, usuario.Cargo.ToString()));
            ci.AddClaim(new Claim("IdUsuario", usuario.Id.ToString()));

            return ci;
        }

        public bool SenhaValida(string senhaRequest, string senhaAtualUsuario)
        {
            return BCrypt.Net.BCrypt.Verify(senhaRequest, senhaAtualUsuario);
        }
        public string GerarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha, 12);
        }
    }
}
