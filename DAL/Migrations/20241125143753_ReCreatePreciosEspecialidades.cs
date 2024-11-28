using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ReCreatePreciosEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renombrar columna de ArticulosId a ArticuloId
            migrationBuilder.RenameColumn(
                name: "ArticulosId",
                table: "PreciosEspecialidades",
                newName: "ArticuloId");

            // Renombrar el índice correspondiente
            migrationBuilder.RenameIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades",
                newName: "IX_PreciosEspecialidades_ArticuloId");

            // Agregar clave foránea para la nueva columna
            migrationBuilder.AddForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticuloId",
                table: "PreciosEspecialidades",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Aseguramos comportamiento adecuado al eliminar
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar clave foránea de ArticuloId
            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticuloId",
                table: "PreciosEspecialidades");

            // Renombrar la columna de vuelta a ArticulosId
            migrationBuilder.RenameColumn(
                name: "ArticuloId",
                table: "PreciosEspecialidades",
                newName: "ArticulosId");

            // Renombrar el índice de vuelta al original
            migrationBuilder.RenameIndex(
                name: "IX_PreciosEspecialidades_ArticuloId",
                table: "PreciosEspecialidades",
                newName: "IX_PreciosEspecialidades_ArticulosId");
        }
    }
}
