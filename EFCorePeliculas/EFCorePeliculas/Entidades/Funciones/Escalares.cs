using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Entidades.Funciones
{
    public class Escalares
    {
        public static void RegistrarFunciones(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => FacturaDetallePromedio(0));
        }

        public static decimal FacturaDetallePromedio(int facturaId)
        {
            return 0;
        }
    }
}
