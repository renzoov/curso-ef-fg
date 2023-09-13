﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(x => x.Identificador);
            builder.Property(x => x.Nombre)
                //.HasColumnName("NombreGenero")
                .HasMaxLength(150)
                .IsRequired();
            //modelBuilder.Entity<Genero>().ToTable("TablaGeneros", "Peliculas"); //nombre tabla y esquema
        }
    }
}