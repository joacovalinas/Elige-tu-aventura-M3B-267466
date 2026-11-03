using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class audiSinNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias");

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "Historias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias",
                column: "HistoriaId",
                principalTable: "Historias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "Historias");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias",
                column: "HistoriaId",
                principalTable: "Historias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
