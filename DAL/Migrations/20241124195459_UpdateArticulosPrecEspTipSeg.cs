using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdateArticulosPrecEspTipSeg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar la restricción y columna actual
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_TiposSegurosId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Articulos");

            // Crear las nuevas columnas necesarias
            migrationBuilder.AddColumn<long>(
                name: "ArticulosId",
                table: "TiposSeguros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ArticulosId",
                table: "PreciosEspecialidades",
                type: "bigint",
                nullable: true);

            // Modificar la columna existente
            migrationBuilder.AlterColumn<long>(
                name: "TiposSegurosId",
                table: "Articulos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            // Crear índices para las nuevas columnas
            migrationBuilder.CreateIndex(
                name: "IX_TiposSeguros_ArticulosId",
                table: "TiposSeguros",
                column: "ArticulosId");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades",
                column: "ArticulosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar las claves foráneas añadidas en el método Up
            migrationBuilder.DropForeignKey(
                name: "FK_TiposSeguros_Articulos_ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticulosId",
                table: "PreciosEspecialidades");

            // Eliminar los índices creados en el método Up
            migrationBuilder.DropIndex(
                name: "IX_TiposSeguros_ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades");

            // Eliminar las columnas añadidas en el método Up
            migrationBuilder.DropColumn(
                name: "ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropColumn(
                name: "ArticulosId",
                table: "PreciosEspecialidades");

            // Restaurar la columna "TiposSegurosId" a su estado original
            migrationBuilder.AlterColumn<long>(
                name: "TiposSegurosId",
                table: "Articulos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            // Restaurar la columna "Nombre"
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Restaurar los índices eliminados en el método Up
            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TiposSegurosId",
                table: "Articulos",
                column: "TiposSegurosId");

            // Restaurar la clave foránea original
            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId",
                table: "Articulos",
                column: "TiposSegurosId",
                principalTable: "TiposSeguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
