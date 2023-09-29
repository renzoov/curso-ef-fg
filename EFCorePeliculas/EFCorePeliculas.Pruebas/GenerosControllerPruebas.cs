using EFCorePeliculas.Controllers;
using EFCorePeliculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class GenerosControllerPruebas : BasePruebas
    {
        [TestMethod]
        public async Task Post_SiEnvioDosGenerosAmbosSonInsertados()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto1 = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto1, mapper: null);
            var generos = new Genero[]
            {
                new Genero(){Nombre = "Genero 1"},
                new Genero(){Nombre = "Genero 2"}
            };

            await generosController.Post(generos);

            var contexto2 = ConstruirContext(nombreDB);
            var generosDB = await contexto2.Generos.ToListAsync();

            Assert.AreEqual(2, generosDB.Count);

            var existeGenero1 = generosDB.Any(g => g.Nombre == "Genero 1");
            Assert.IsTrue(existeGenero1, message: "El genero 1 no fue encontrado");

            var existeGenero2 = generosDB.Any(g => g.Nombre == "Genero 2");
            Assert.IsTrue(existeGenero2, message: "El genero 2 no fue encontrado");
        }

        [TestMethod]
        public async Task Put_SiEnvioUnGeneroNombreOriginalIncorrecto_UnaExcepcionEsArrojada()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto1 = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();
            var generoPrueba = new Genero() { Nombre = "Genero 1" };
            contexto1.Add(generoPrueba);
            await contexto1.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto2, mapper);

            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(() =>
                generosController.Put(new DTOs.GeneroActualizacionDTO()
                {
                    Identificador = generoPrueba.Identificador,
                    Nombre = "Genero 2",
                    Nombre_Original = "Nombre incorrecto"
                }));
        }

        [TestMethod]
        public async Task Put_SiEnvioUnGeneroNombreOriginalCorrecto_EntoncesSeActualizaElGenero()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto1 = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();
            var generoPrueba = new Genero() { Nombre = "Genero 1" };
            contexto1.Add(generoPrueba);
            await contexto1.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto2, mapper);

            await generosController.Put(new DTOs.GeneroActualizacionDTO()
            {
                Identificador = generoPrueba.Identificador,
                Nombre = "Genero 2",
                Nombre_Original = "Genero 1"
            });

            var contexto3 = ConstruirContext(nombreDB);
            var generoDB = await contexto3.Generos.SingleAsync();
            Assert.AreEqual(generoPrueba.Identificador, generoDB.Identificador);
            Assert.AreEqual("Genero 2", generoDB.Nombre);
        }
    }
}
