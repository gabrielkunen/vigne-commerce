using VigneCommerce.Domain.Entities.Base;
using VigneCommerce.Domain.Enums;

namespace VigneCommerce.Domain.Entities
{
    public class Pedido(EFormaPagamento formaPagamento) : EntityBase
    {
        public int Id { get; private set; }
        public List<PedidoItem> PedidoItens { get; private set; } = [];
        public DateTime DataPedido { get; private set; } = DateTime.Now;
        public EFormaPagamento FormaPagamento { get; private set; } = formaPagamento;
        public PedidoEndereco EnderecoEntrega { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario UsuarioPedido { get; private set; }

        public void AdicionarItemAoPedido(PedidoItem item)
        {
            PedidoItens.Add(item);
        }

        public void AdicionarEnderecoEntrega(PedidoEndereco endereco)
        {
            EnderecoEntrega = endereco;
        }

        public void SetarUsuarioRealizouPedido(int usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
