using VigneCommerce.Domain.Enums;

namespace VigneCommerce.Api.Request
{
    public class CadastrarPedidoRequest
    {
        public List<CadastrarPedidoItemRequest> IdProdutos { get; set; }
        public CadastrarPedidoEnderecoRequest EnderecoEntrega { get; set; }
        public EFormaPagamento FormaPagamento { get; set; }
    }

    public class CadastrarPedidoEnderecoRequest
    {
        public string Cep { get; set; }
        public string DescricaoEndereco { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
    }

    public class CadastrarPedidoItemRequest
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
