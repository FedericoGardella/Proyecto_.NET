using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticulosPrecEspTipSeg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Especialidades_EspecialidadesId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_EspecialidadesId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_TiposSegurosId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "EspecialidadesId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Articulos");

            migrationBuilder.AddColumn<long>(
                name: "ArticulosId",
                table: "TiposSeguros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EspecialidadesId",
                table: "PreciosEspecialidades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TiposSegurosId",
                table: "PreciosEspecialidades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "TiposSegurosId",
                table: "Articulos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PreciosEspecialidadesId",
                table: "Articulos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposSeguros_ArticulosId",
                table: "TiposSeguros",
                column: "ArticulosId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades",
                column: "ArticulosId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreciosEspecialidades_EspecialidadesId",
                table: "PreciosEspecialidades",
                column: "EspecialidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosEspecialidades_TiposSegurosId",
                table: "PreciosEspecialidades",
                column: "TiposSegurosId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosEspecialidades_Especialidades_EspecialidadesId",
                table: "PreciosEspecialidades",
                column: "EspecialidadesId",
                principalTable: "Especialidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosEspecialidades_TiposSeguros_TiposSegurosId",
                table: "PreciosEspecialidades",
                column: "TiposSegurosId",
                principalTable: "TiposSeguros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposSeguros_Articulos_ArticulosId",
                table: "TiposSeguros",
                column: "ArticulosId",
                principalTable: "Articulos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_Especialidades_EspecialidadesId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_PreciosEspecialidades_TiposSeguros_TiposSegurosId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposSeguros_Articulos_ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropIndex(
                name: "IX_TiposSeguros_ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropIndex(
                name: "IX_PreciosEspecialidades_EspecialidadesId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropIndex(
                name: "IX_PreciosEspecialidades_TiposSegurosId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropColumn(
                name: "ArticulosId",
                table: "TiposSeguros");

            migrationBuilder.DropColumn(
                name: "EspecialidadesId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropColumn(
                name: "TiposSegurosId",
                table: "PreciosEspecialidades");

            migrationBuilder.DropColumn(
                name: "PreciosEspecialidadesId",
                table: "Articulos");

            migrationBuilder.AlterColumn<long>(
                name: "TiposSegurosId",
                table: "Articulos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EspecialidadesId",
                table: "Articulos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosEspecialidades_ArticulosId",
                table: "PreciosEspecialidades",
                column: "ArticulosId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_EspecialidadesId",
                table: "Articulos",
                column: "EspecialidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TiposSegurosId",
                table: "Articulos",
                column: "TiposSegurosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Especialidades_EspecialidadesId",
                table: "Articulos",
                column: "EspecialidadesId",
                principalTable: "Especialidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_TiposSeguros_TiposSegurosId",
                table: "Articulos",
                column: "TiposSegurosId",
                principalTable: "TiposSeguros",
                principalColumn: "Id");
        }
    }
}
