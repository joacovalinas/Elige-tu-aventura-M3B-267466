using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class audi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalle",
                table: "Auditorias");

            migrationBuilder.DropColumn(
                name: "Entidad",
                table: "Auditorias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detalle",
                table: "Auditorias",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Entidad",
                table: "Auditorias",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
