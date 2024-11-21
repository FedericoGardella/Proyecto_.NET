using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RelacionRecetasMedicamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Recetas_RecetasId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_RecetasId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "RecetasId",
                table: "Medicamentos");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Recetas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecetasMedicamentos",
                columns: table => new
                {
                    MedicamentosId = table.Column<long>(type: "bigint", nullable: false),
                    RecetasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetasMedicamentos", x => new { x.MedicamentosId, x.RecetasId });
                    table.ForeignKey(
                        name: "FK_RecetasMedicamentos_Medicamentos_MedicamentosId",
                        column: x => x.MedicamentosId,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetasMedicamentos_Recetas_RecetasId",
                        column: x => x.RecetasId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetasMedicamentos_RecetasId",
                table: "RecetasMedicamentos",
                column: "RecetasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecetasMedicamentos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Recetas");

            migrationBuilder.AddColumn<long>(
                name: "RecetasId",
                table: "Medicamentos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_RecetasId",
                table: "Medicamentos",
                column: "RecetasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Recetas_RecetasId",
                table: "Medicamentos",
                column: "RecetasId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
