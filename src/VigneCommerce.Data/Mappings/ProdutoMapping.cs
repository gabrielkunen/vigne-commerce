using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable(nameof(Produto));

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Descricao)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(16,2)");

            builder.Property(p => p.QuantidadeEstoque)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
