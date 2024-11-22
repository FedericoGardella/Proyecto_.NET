using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionRelacionPacienteHistoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_HistoriasClinicas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "HistoriasClinicasId",
                table: "Personas");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_PacientesId",
                table: "HistoriasClinicas",
                column: "PacientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoriasClinicas_Personas_PacientesId",
                table: "HistoriasClinicas",
                column: "PacientesId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoriasClinicas_Personas_PacientesId",
                table: "HistoriasClinicas");

            migrationBuilder.DropIndex(
                name: "IX_HistoriasClinicas_PacientesId",
                table: "HistoriasClinicas");

            migrationBuilder.AddColumn<long>(
                name: "HistoriasClinicasId",
                table: "Personas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_HistoriasClinicasId",
                table: "Personas",
                column: "HistoriasClinicasId",
                unique: true,
                filter: "[HistoriasClinicasId] IS NOT NULL");

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
