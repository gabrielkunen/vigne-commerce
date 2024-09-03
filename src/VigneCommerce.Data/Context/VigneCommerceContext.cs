using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Context
{
    public class VigneCommerceContext : DbContext
    {
        public VigneCommerceContext(DbContextOptions<VigneCommerceContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoEndereco> PedidoEnderecos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
