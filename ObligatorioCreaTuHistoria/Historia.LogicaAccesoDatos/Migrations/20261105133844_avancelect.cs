using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class avancelect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "finalizado",
                table: "Lecturas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "finalizado",
                table: "Lecturas");
        }
    }
}
