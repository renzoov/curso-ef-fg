using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.Configuraciones;
using EFCorePeliculas.Entidades.Configuraciones.Seeding;
using EFCorePeliculas.Entidades.SinLlaves;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCorePeliculas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new GeneroConfig()); // 1ra forma agregar de 1 en 1
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // 2da forma agregar todas las configuraciones

            SeedingModuloConsulta.Seed(modelBuilder);
            SeedingPersonaMensaje.Seed(modelBuilder);

            //modelBuilder.Entity<Actor>().Property(x => x.FechaNacimiento)
            //    .HasColumnType("date");

            //modelBuilder.Entity<Pelicula>().Property(x => x.FechaEstreno)
            //    .HasColumnType("date");

            //modelBuilder.Entity<CineOferta>().Property(x => x.FechaInicio)
            //    .HasColumnType("date");

            //modelBuilder.Entity<CineOferta>().Property(x => x.FechaFin)
            //    .HasColumnType("date");

            //modelBuilder.Entity<Log>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<CineSinUbicacion>().HasNoKey().ToSqlQuery("select id, nombre from Cines").ToView(null);

            modelBuilder.Entity<PeliculaConConteos>().HasNoKey().ToView("PeliculasConConteos");

            foreach(var tipoEntidad in modelBuilder.Model.GetEntityTypes())
            {
                foreach(var propiedad in tipoEntidad.GetProperties())
                {
                    if(propiedad.ClrType == typeof(string) && propiedad.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        propiedad.SetIsUnicode(false);
                        propiedad.SetMaxLength(500);
                    }
                }
            }
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<SalaDeCine> SalasDeCine { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set;}
        public DbSet<CineSinUbicacion> CinesSinUbicacion { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<CineDetalle> CineDetalle { get; set; }
    }
}
