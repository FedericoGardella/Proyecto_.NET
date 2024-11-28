using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateArticulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminamos la tabla "Articulo" si existe.
            migrationBuilder.DropTable(
                name: "Articulo");

            // Modificamos la columna "Costo" de la tabla "Articulos" para ajustar precisión y escala.
            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Articulos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restauramos la columna "Costo" con su precisión y escala originales.
            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            // Restauramos la tabla "Articulo".
            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecialidadId = table.Column<long>(type: "bigint", nullable: false),
                    TipoSeguroId = table.Column<long>(type: "bigint", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulo_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulo_TipoSeguro_TipoSeguroId",
                        column: x => x.TipoSeguroId,
                        principalTable: "TiposSeguros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Crear índices relacionados con "EspecialidadId" y "TipoSeguroId".
            migrationBuilder.CreateIndex(
                name: "IX_Articulo_EspecialidadId",
                table: "Articulo",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_TipoSeguroId",
                table: "Articulo",
                column: "TipoSeguroId");
        }
    }
}
