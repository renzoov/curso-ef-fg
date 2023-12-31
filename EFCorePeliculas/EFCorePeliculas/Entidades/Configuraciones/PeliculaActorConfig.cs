﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            builder.HasKey(x => new { x.ActorId, x.PeliculaId });

            //builder.HasOne(x => x.Actor)
            //    .WithMany(x => x.PeliculasActores)
            //    .HasForeignKey(x => x.ActorId);

            //builder.HasOne(x => x.Pelicula)
            //    .WithMany(x => x.PeliculasActores)
            //    .HasForeignKey(x => x.PeliculaId);

            builder.Property(x => x.Personaje)
                .HasMaxLength(150);
        }
    }
}
