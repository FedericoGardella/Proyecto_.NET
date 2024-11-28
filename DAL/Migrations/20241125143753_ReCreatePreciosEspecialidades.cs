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
            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticulosId",
                table: "PreciosEspecialidades");

            migrationBuilder.RenameColumn(
                name: "ArticulosId",
                table: "PreciosEspecialidades",
                newName: "ArticuloId");

            migrationBuilder.RenameIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades",
                newName: "IX_PreciosEspecialidades_ArticuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticuloId",
                table: "PreciosEspecialidades",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticuloId",
                table: "PreciosEspecialidades");

            migrationBuilder.RenameColumn(
                name: "ArticuloId",
                table: "PreciosEspecialidades",
                newName: "ArticulosId");

            migrationBuilder.RenameIndex(
                name: "IX_PreciosEspecialidades_ArticuloId",
                table: "PreciosEspecialidades",
                newName: "IX_PreciosEspecialidades_ArticulosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosEspecialidades_Articulos_ArticulosId",
                table: "PreciosEspecialidades",
                column: "ArticulosId",
                principalTable: "Articulos",
                principalColumn: "Id");
        }
    }
}
