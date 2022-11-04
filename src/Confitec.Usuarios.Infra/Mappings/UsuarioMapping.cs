using Confitec.Usuarios.Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confitec.Usuarios.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.SobreNome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(254)");

            builder.Property(x => x.DataNascimento)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.Escolaridade)
                .IsRequired()
                .HasColumnType("int");


            builder.ToTable("Usuario");
        }
    }
}