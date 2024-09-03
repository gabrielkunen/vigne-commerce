using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Domain.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task<int> Adicionar(Pedido pedido);
    }
}
