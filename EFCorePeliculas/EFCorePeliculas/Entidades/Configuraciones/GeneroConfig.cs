using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable(name: "Generos", opciones =>
            {
                opciones.IsTemporal();
            });

            builder.Property<DateTime>("PeriodStart").HasColumnType("datetime2");
            builder.Property<DateTime>("PeriodEnd").HasColumnType("datetime2");

            builder.HasKey(x => x.Identificador);
            builder.Property(x => x.Nombre)
                //.IsConcurrencyToken()
                //.HasColumnName("NombreGenero")
                .HasMaxLength(150)
                .IsRequired();
            //modelBuilder.Entity<Genero>().ToTable("TablaGeneros", "Peliculas"); //nombre tabla y esquema

            builder.HasQueryFilter(g => !g.EstaBorrado);

            builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("EstaBorrado = false");

            builder.Property<DateTime>("FechaCreacion").HasDefaultValueSql("GetDate()").HasColumnType("datetime2");
        }
    }
}
