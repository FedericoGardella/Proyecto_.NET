using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RelacionMedicosEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadesMedicos_Especialidades_EspecialidadesId",
                table: "EspecialidadesMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadesMedicos_Personas_MedicosId",
                table: "EspecialidadesMedicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EspecialidadesMedicos",
                table: "EspecialidadesMedicos");

            migrationBuilder.RenameTable(
                name: "EspecialidadesMedicos",
                newName: "MedicosEspecialidades");

            migrationBuilder.RenameIndex(
                name: "IX_EspecialidadesMedicos_MedicosId",
                table: "MedicosEspecialidades",
                newName: "IX_MedicosEspecialidades_MedicosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicosEspecialidades",
                table: "MedicosEspecialidades",
                columns: new[] { "EspecialidadesId", "MedicosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicosEspecialidades_Especialidades_EspecialidadesId",
                table: "MedicosEspecialidades",
                column: "EspecialidadesId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicosEspecialidades_Personas_MedicosId",
                table: "MedicosEspecialidades",
                column: "MedicosId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicosEspecialidades_Especialidades_EspecialidadesId",
                table: "MedicosEspecialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicosEspecialidades_Personas_MedicosId",
                table: "MedicosEspecialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicosEspecialidades",
                table: "MedicosEspecialidades");

            migrationBuilder.RenameTable(
                name: "MedicosEspecialidades",
                newName: "EspecialidadesMedicos");

            migrationBuilder.RenameIndex(
                name: "IX_MedicosEspecialidades_MedicosId",
                table: "EspecialidadesMedicos",
                newName: "IX_EspecialidadesMedicos_MedicosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EspecialidadesMedicos",
                table: "EspecialidadesMedicos",
                columns: new[] { "EspecialidadesId", "MedicosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadesMedicos_Especialidades_EspecialidadesId",
                table: "EspecialidadesMedicos",
                column: "EspecialidadesId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadesMedicos_Personas_MedicosId",
                table: "EspecialidadesMedicos",
                column: "MedicosId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
