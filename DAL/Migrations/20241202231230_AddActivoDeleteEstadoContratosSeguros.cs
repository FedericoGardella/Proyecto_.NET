using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddActivoDeleteEstadoContratosSeguros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ContratosSeguros");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ContratosSeguros",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ContratosSeguros");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ContratosSeguros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
