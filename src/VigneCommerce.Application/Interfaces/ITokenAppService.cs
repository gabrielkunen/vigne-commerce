using VigneCommerce.Application.Dto;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Application.Interfaces
{
    public interface ITokenAppService
    {
        LoginTokenDto GerarToken(Usuario usuario, string chaveToken);
        bool SenhaValida(string senhaRequest, string senhaAtualUsuario);
        string GerarSenha(string senha);
    }
}
