using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class auditorias2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntidadId",
                table: "Auditorias",
                newName: "HistoriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Entidad",
                table: "Auditorias",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_HistoriaId",
                table: "Auditorias",
                column: "HistoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias",
                column: "HistoriaId",
                principalTable: "Historias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Historias_HistoriaId",
                table: "Auditorias");

            migrationBuilder.DropIndex(
                name: "IX_Auditorias_HistoriaId",
                table: "Auditorias");

            migrationBuilder.RenameColumn(
                name: "HistoriaId",
                table: "Auditorias",
                newName: "EntidadId");

            migrationBuilder.AlterColumn<string>(
                name: "Entidad",
                table: "Auditorias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
