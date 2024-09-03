using VigneCommerce.Domain.Entities.Base;

namespace VigneCommerce.Domain.Entities
{
    public class PedidoItem(string nome, decimal valor, int quantidade) : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; private set; } = nome;
        public decimal Valor { get; private set; } = valor;
        public int Quantidade { get; private set; } = quantidade;
        public int PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
    }
}
