using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<int> Adicionar(Usuario usuario);
        bool JaExiste(string email);
        Task<Usuario?> BuscarPorEmail(string email);
    }
}
