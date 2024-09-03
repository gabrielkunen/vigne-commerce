namespace VigneCommerce.Application.Dto
{
    public class LoginTokenDto(string token, DateTime dataExpiracao)
    {
        public string Token { get; set; } = token;
        public DateTime DataExpiracao { get; set; } = dataExpiracao;
    }
}
