using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ArreglosPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_ContratosSeguros_ContratosSegurosId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_ContratosSeguros_ContratosSegurosId",
                table: "Personas",
                column: "ContratosSegurosId",
                principalTable: "ContratosSeguros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas",
                column: "HistoriasClinicasId",
                principalTable: "HistoriasClinicas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_ContratosSeguros_ContratosSegurosId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_ContratosSeguros_ContratosSegurosId",
                table: "Personas",
                column: "ContratosSegurosId",
                principalTable: "ContratosSeguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas",
                column: "HistoriasClinicasId",
                principalTable: "HistoriasClinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
