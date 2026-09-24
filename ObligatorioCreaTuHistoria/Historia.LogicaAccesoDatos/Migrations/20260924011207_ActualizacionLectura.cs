using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionLectura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "lecturas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Nombrelectura",
                table: "lecturas",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombrelectura",
                table: "lecturas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "lecturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
