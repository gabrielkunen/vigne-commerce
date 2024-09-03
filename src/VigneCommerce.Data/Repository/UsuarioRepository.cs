using Microsoft.EntityFrameworkCore;
using VigneCommerce.Data.Context;
using VigneCommerce.Domain.Entities;
using VigneCommerce.Domain.Interfaces.Repository;

namespace VigneCommerce.Data.Repository
{
    public class UsuarioRepository(VigneCommerceContext context) : IUsuarioRepository
    {
        private readonly VigneCommerceContext _context = context;

        public async Task<int> Adicionar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id;
        }

        public bool JaExiste(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }

        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
