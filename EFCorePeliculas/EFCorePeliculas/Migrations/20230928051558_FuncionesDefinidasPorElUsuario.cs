using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
    public partial class FuncionesDefinidasPorElUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION FacturaDetalleSuma
                (
                @FacturaId INT
                )
                RETURNS INT
                AS
                BEGIN
                
                DECLARE @suma INT;
                
                SELECT @suma = SUM(Precio)
                FROM FacturaDetalles
                WHERE FacturaId = @FacturaId

                RETURN @suma
                END");

            migrationBuilder.Sql(@"
                CREATE FUNCTION FacturaDetallePromedio
                (
                @FacturaId INT
                )
                RETURNS INT
                AS
                BEGIN

                DECLARE @promedio DECIMAL(18,2);

                SELECT @promedio = AVG(Precio)
                FROM FacturaDetalles
                WHERE FacturaId = @FacturaId

                RETURN @promedio
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [dbo].[FacturaDetalleSuma]");
            migrationBuilder.Sql("DROP FUNCTION [dbo].[FacturaDetallePromedio]");
        }
    }
}
