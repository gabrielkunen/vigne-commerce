using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VigneCommerce.Domain.Entities;

namespace VigneCommerce.Data.Mappings
{
    internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(Usuario));

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Senha)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.Cargo)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
