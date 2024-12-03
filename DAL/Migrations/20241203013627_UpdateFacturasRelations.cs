using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacturasRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Facturas_FacturasId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Facturas_FacturasId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_FacturasId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_FacturasId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "FacturasId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "FacturasId",
                table: "Citas");

            migrationBuilder.AddColumn<long>(
                name: "CitasId",
                table: "Facturas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContratosSegurosId",
                table: "Facturas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CitasId",
                table: "Facturas",
                column: "CitasId",
                unique: true,
                filter: "[CitasId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ContratosSegurosId",
                table: "Facturas",
                column: "ContratosSegurosId",
                unique: true,
                filter: "[ContratosSegurosId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PacientesId",
                table: "Facturas",
                column: "PacientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Citas_CitasId",
                table: "Facturas",
                column: "CitasId",
                principalTable: "Citas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_ContratosSeguros_ContratosSegurosId",
                table: "Facturas",
                column: "ContratosSegurosId",
                principalTable: "ContratosSeguros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Personas_PacientesId",
                table: "Facturas",
                column: "PacientesId",
                principalTable: "Personas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Citas_CitasId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_ContratosSeguros_ContratosSegurosId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Personas_PacientesId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_CitasId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ContratosSegurosId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_PacientesId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "CitasId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "ContratosSegurosId",
                table: "Facturas");

            migrationBuilder.AddColumn<long>(
                name: "FacturasId",
                table: "Personas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacturasId",
                table: "Citas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_FacturasId",
                table: "Personas",
                column: "FacturasId",
                unique: true,
                filter: "[FacturasId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_FacturasId",
                table: "Citas",
                column: "FacturasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Facturas_FacturasId",
                table: "Citas",
                column: "FacturasId",
                principalTable: "Facturas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Facturas_FacturasId",
                table: "Personas",
                column: "FacturasId",
                principalTable: "Facturas",
                principalColumn: "Id");
        }
    }
}
