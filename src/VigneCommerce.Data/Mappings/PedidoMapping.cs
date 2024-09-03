using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable(nameof(Pedido));

            builder.HasMany(p => p.PedidoItens)
                .WithOne(pi => pi.Pedido)
                .HasForeignKey(pi => pi.PedidoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.DataPedido)
                .IsRequired();

            builder.Property(p => p.FormaPagamento)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(p => p.EnderecoEntrega)
                .WithOne(e => e.Pedido)
                .HasForeignKey<PedidoEndereco>(b => b.PedidoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.UsuarioPedido)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
