using EFCorePeliculas.Servicios;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class ServicioUsuarioPruebas
    {
        [TestMethod]
        public void ObtenerUsuarioId_noTraeValorVacioONulo()
        {
            // Preparación
            var servicioUsuario = new ServicioUsuario();

            // Prueba
            var resultado = servicioUsuario.ObtenerUsuarioId();

            // Verificación
            Assert.AreNotEqual("", resultado);
            Assert.IsNotNull(resultado);
        }
    }
}