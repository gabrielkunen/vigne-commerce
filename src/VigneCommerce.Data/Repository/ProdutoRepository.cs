using Microsoft.EntityFrameworkCore;
using VigneCommerce.Data.Context;
using VigneCommerce.Domain.Entities;
using VigneCommerce.Domain.Interfaces.Repository;

namespace VigneCommerce.Data.Repository
{
    public class ProdutoRepository(VigneCommerceContext context) : IProdutoRepository
    {
        private readonly VigneCommerceContext _context = context;

        public async Task<Produto?> BuscarPorId(int id) => await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto.Id;
        }

        public List<Produto> Buscar(int take, int skip)
        {
            return [.. _context.Produtos.Skip(skip).Take(take)];
        }

        public async Task<int> Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto.Id;
        }

        public async Task DebitarEstoque(int produtoId, int quantidade)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId);

            if (produto == null)
                return;

            produto.DebitarEstoque(quantidade);
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}
