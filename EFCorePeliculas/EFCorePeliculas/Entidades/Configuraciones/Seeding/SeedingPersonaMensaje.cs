using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Entidades.Configuraciones.Seeding
{
    public static class SeedingPersonaMensaje
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var felipe = new Persona() { Id = 1, Nombre = "Felipe" };
            var pepe = new Persona() { Id = 2, Nombre = "Pepe" };

            var mensaje1 = new Mensaje() { Id = 1, Contenido = "Hola Pepe!", EmisorId = felipe.Id, ReceptorId = pepe.Id };
            var mensaje2 = new Mensaje() { Id = 2, Contenido = "Hola Felipe, ¿Cómo estas?", EmisorId = pepe.Id, ReceptorId = felipe.Id };
            var mensaje3 = new Mensaje() { Id = 3, Contenido = "Todo bien, ¿ Y tú?", EmisorId = felipe.Id, ReceptorId = pepe.Id };
            var mensaje4 = new Mensaje() { Id = 4, Contenido = "Muy bien", EmisorId = pepe.Id, ReceptorId = felipe.Id };

            modelBuilder.Entity<Persona>().HasData(felipe, pepe);
            modelBuilder.Entity<Mensaje>().HasData(mensaje1, mensaje2, mensaje3, mensaje4);
        }
    }
}
