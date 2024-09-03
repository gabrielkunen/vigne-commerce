using VigneCommerce.Data.Context;
using VigneCommerce.Domain.Entities;
using VigneCommerce.Domain.Interfaces.Repository;

namespace VigneCommerce.Data.Repository
{
    public class PedidoRepository(VigneCommerceContext context) : IPedidoRepository
    {
        private readonly VigneCommerceContext _context = context;
        public async Task<int> Adicionar(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id;
        }
    }
}
