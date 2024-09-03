using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<PedidoEndereco>
    {
        public void Configure(EntityTypeBuilder<PedidoEndereco> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable(nameof(PedidoEndereco));

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(p => p.DescricaoEndereco)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Bairro)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
