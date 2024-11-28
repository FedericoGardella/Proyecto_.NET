using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FacturasMes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_ContratosSeguros_ContratosSegurosId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ContratosSegurosId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "FechaEmision",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Facturas");

            migrationBuilder.RenameColumn(
                name: "ContratosSegurosId",
                table: "Facturas",
                newName: "PacientesId");

            migrationBuilder.CreateTable(
                name: "FacturasMes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GastosMes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagada = table.Column<bool>(type: "bit", nullable: false),
                    ContratosSegurosId = table.Column<long>(type: "bigint", nullable: false),
                    CostoContrato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacturasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasMes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasMes_ContratosSeguros_ContratosSegurosId",
                        column: x => x.ContratosSegurosId,
                        principalTable: "ContratosSeguros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacturasMes_Facturas_FacturasId",
                        column: x => x.FacturasId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PacientesId",
                table: "Facturas",
                column: "PacientesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacturasMes_ContratosSegurosId",
                table: "FacturasMes",
                column: "ContratosSegurosId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasMes_FacturasId",
                table: "FacturasMes",
                column: "FacturasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Personas_PacientesId",
                table: "Facturas",
                column: "PacientesId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Personas_PacientesId",
                table: "Facturas");

            migrationBuilder.DropTable(
                name: "FacturasMes");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_PacientesId",
                table: "Facturas");

            migrationBuilder.RenameColumn(
                name: "PacientesId",
                table: "Facturas",
                newName: "ContratosSegurosId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEmision",
                table: "Facturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Facturas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ContratosSegurosId",
                table: "Facturas",
                column: "ContratosSegurosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_ContratosSeguros_ContratosSegurosId",
                table: "Facturas",
                column: "ContratosSegurosId",
                principalTable: "ContratosSeguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
