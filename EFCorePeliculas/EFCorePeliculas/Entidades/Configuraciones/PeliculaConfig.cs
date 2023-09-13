﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {

            builder.Property(x => x.Titulo)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.PosterURL)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
