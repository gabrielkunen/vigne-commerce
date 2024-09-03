using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.ToTable(nameof(PedidoItem));

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(16,2)");

            builder.Property(p => p.Quantidade)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
