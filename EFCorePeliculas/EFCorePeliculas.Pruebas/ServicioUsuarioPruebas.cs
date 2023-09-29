using EFCorePeliculas.Servicios;

namespace EFCorePeliculas.Pruebas
{
    [TestClass]
    public class ServicioUsuarioPruebas
    {
        [TestMethod]
        public void ObtenerUsuarioId_noTraeValorVacioONulo()
        {
            // Preparaci�n
            var servicioUsuario = new ServicioUsuario();

            // Prueba
            var resultado = servicioUsuario.ObtenerUsuarioId();

            // Verificaci�n
            Assert.AreNotEqual("", resultado);
            Assert.IsNotNull(resultado);
        }
    }
}