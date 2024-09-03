namespace VigneCommerce.Api.Request
{
    public class CadastrarProdutoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
