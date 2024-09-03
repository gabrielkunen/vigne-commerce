using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Domain.Interfaces.Repository
{
    public interface IProdutoRepository
    {
        Task<Produto?> BuscarPorId(int id);
        Task<int> Adicionar(Produto produto);
        List<Produto> Buscar(int take, int skip);
        Task<int> Atualizar(Produto produto);
        Task DebitarEstoque(int produtoId, int quantidade);
    }
}
