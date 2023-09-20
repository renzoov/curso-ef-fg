using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class CineConfig : IEntityTypeConfiguration<Cine>
    {
        public void Configure(EntityTypeBuilder<Cine> builder)
        {
            builder.Property(x => x.Nombre)
                .HasMaxLength(150)
                .IsRequired();

            //builder.HasOne(x => x.CineOferta)
            //    .WithOne()
            //    .HasForeignKey<CineOferta>(x => x.CineId);

            //builder.HasMany(x => x.SalasDeCine)
            //    .WithOne(x => x.Cine)
            //    .HasForeignKey(x => x.CineId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CineDetalle)
                .WithOne(x => x.Cine)
                .HasForeignKey<CineDetalle>(x => x.Id);

            builder.OwnsOne(x => x.Direccion, dir =>
            {
                dir.Property(d => d.Calle).HasColumnName("Calle");
                dir.Property(d => d.Provincia).HasColumnName("Provincia");
                dir.Property(d => d.Pais).HasColumnName("Pais");
            });
        }
    }
}
