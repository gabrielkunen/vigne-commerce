namespace VigneCommerce.Api.Response
{
    public class LoginUsuarioResponse(string token, DateTime dataExpiracao)
    {
        public string Token { get; set; } = token;
        public DateTime DataExpiracao { get; set; } = dataExpiracao;
    }
}
