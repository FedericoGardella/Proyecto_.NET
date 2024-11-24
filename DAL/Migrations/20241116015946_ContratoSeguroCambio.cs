using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ContratoSeguroCambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Elimina cualquier configuración incorrecta anterior
            migrationBuilder.DropIndex(
                name: "IX_Personas_ContratosSegurosId",
                table: "Personas");

            // Agrega la columna PacienteId en ContratosSeguros con clave foránea
            migrationBuilder.AddColumn<long>(
                name: "PacienteId",
                table: "ContratosSeguros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            // Configura la relación de uno a muchos (Pacientes -> ContratosSeguros)
            migrationBuilder.CreateIndex(
                name: "IX_ContratosSeguros_PacienteId",
                table: "ContratosSeguros",
                column: "PacienteId");

            // Define la clave foránea de ContratosSeguros a Pacientes
            migrationBuilder.AddForeignKey(
                name: "FK_ContratosSeguros_Personas_PacienteId",
                table: "ContratosSeguros",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir los cambios realizados en la migración
            migrationBuilder.DropForeignKey(
                name: "FK_ContratosSeguros_Personas_PacienteId",
                table: "ContratosSeguros");

            migrationBuilder.DropIndex(
                name: "IX_ContratosSeguros_PacienteId",
                table: "ContratosSeguros");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "ContratosSeguros");

            // Restaurar el índice anterior en Personas si es necesario
            migrationBuilder.CreateIndex(
                name: "IX_Personas_ContratosSegurosId",
                table: "Personas",
                column: "ContratosSegurosId",
                unique: true,
                filter: "[ContratosSegurosId] IS NOT NULL");
        }
    }
}
