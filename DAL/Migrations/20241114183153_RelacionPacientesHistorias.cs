using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RelacionPacientesHistorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas",
                column: "HistoriasClinicasId",
                principalTable: "HistoriasClinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas",
                column: "HistoriasClinicasId",
                principalTable: "HistoriasClinicas",
                principalColumn: "Id");
        }
    }
}
