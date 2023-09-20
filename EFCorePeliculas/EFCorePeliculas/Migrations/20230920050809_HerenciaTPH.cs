using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
    public partial class HerenciaTPH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "date", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Ultimos4Digitos = table.Column<string>(type: "char(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "CorreoElectronico", "FechaTransaccion", "Monto", "TipoPago" },
                values: new object[,]
                {
                    { 3, "felipe@gmail.com", new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 157m, 1 },
                    { 4, "juan@gmail.com", new DateTime(2022, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 9.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "Monto", "TipoPago", "Ultimos4Digitos" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m, 2, "0123" },
                    { 2, new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 2, "1234" }
                });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 19, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaEstreno",
                value: new DateTime(2023, 9, 19, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
