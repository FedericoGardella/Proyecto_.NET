using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropTable(
                name: "TipoSeguro");

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Articulos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_TiposSegurosId1",
                table: "Articulos");

            // Eliminar la clave foránea dependiente de la columna
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId1",
                table: "Articulos");

            // Eliminar la columna
            migrationBuilder.DropColumn(
                name: "TiposSegurosId1",
                table: "Articulos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tiempoCita = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSeguro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSeguro", x => x.Id);
                });

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
                        principalTable: "Especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulo_TipoSeguro_TipoSeguroId",
                        column: x => x.TipoSeguroId,
                        principalTable: "TipoSeguro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_EspecialidadId",
                table: "Articulo",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_TipoSeguroId",
                table: "Articulo",
                column: "TipoSeguroId");

            // Restaurar la columna
            migrationBuilder.AddColumn<long>(
                name: "TiposSegurosId1",
                table: "Articulos",
                type: "bigint",
                nullable: true);

            // Restaurar la clave foránea
            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId1",
                table: "Articulos",
                column: "TiposSegurosId1",
                principalTable: "TiposSeguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Restaurar el índice
            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TiposSegurosId1",
                table: "Articulos",
                column: "TiposSegurosId1");
        }
    }
}
