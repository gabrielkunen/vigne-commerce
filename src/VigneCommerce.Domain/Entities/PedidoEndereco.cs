using VigneCommerce.Domain.Entities.Base;

namespace VigneCommerce.Domain.Entities
{
    public class PedidoEndereco(string cep, string descricaoEndereco, string bairro, int numero) : EntityBase
    {
        public int Id { get; private set; }
        public string Cep { get; private set; } = cep;
        public string DescricaoEndereco { get; private set; } = descricaoEndereco;
        public string Bairro { get; private set; } = bairro;
        public int Numero { get; private set; } = numero;
        public int PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
    }
}
