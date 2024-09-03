namespace VigneCommerce.Api.Response
{
    public class BuscarProdutoResponse(string nome, string descricao, decimal valor, int quantidadeEstoque)
    {
        public string Nome { get; set; } = nome;
        public string Descricao { get; set; } = descricao;
        public decimal valor { get; set; } = valor;
        public int QuantidadeEstoque { get; set; } = quantidadeEstoque;
    }
}
