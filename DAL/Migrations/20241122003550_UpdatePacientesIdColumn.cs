using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePacientesIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renombrar la columna de "PacienteId" a "PacientesId"
            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "ContratosSeguros",
                newName: "PacientesId");

            // Si existe un índice o clave foránea relacionada, también debes renombrarla
            migrationBuilder.RenameIndex(
                name: "IX_ContratosSeguros_PacienteId",
                table: "ContratosSeguros",
                newName: "IX_ContratosSeguros_PacientesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir el cambio en caso de que sea necesario deshacer la migración
            migrationBuilder.RenameColumn(
                name: "PacientesId",
                table: "ContratosSeguros",
                newName: "PacienteId");

            // Revertir el nombre del índice
            migrationBuilder.RenameIndex(
                name: "IX_ContratosSeguros_PacientesId",
                table: "ContratosSeguros",
                newName: "IX_ContratosSeguros_PacienteId");
        }
    }
}
