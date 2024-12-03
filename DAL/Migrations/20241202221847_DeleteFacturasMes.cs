using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFacturasMes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturasMes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturasMes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContratosSegurosId = table.Column<long>(type: "bigint", nullable: false),
                    FacturasId = table.Column<long>(type: "bigint", nullable: false),
                    CostoContrato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GastosMes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pagada = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_FacturasMes_ContratosSegurosId",
                table: "FacturasMes",
                column: "ContratosSegurosId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasMes_FacturasId",
                table: "FacturasMes",
                column: "FacturasId");
        }
    }
}
