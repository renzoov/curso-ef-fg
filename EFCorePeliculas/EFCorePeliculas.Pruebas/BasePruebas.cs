using AutoMapper;
using EFCorePeliculas.Servicios;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Pruebas
{
    public class BasePruebas
    {
        protected ApplicationDbContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var servicioUsuario = new ServicioUsuario();

            var dbContext = new ApplicationDbContext(opciones, servicioUsuario, eventosDbContext: null);

            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(opciones =>
            {
                opciones.AddProfile(new AutoMapperProfile());
            });

            return config.CreateMapper();
        }
    }
}
