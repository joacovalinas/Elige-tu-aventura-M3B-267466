using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscogeAventura.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class pasoout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasoLectura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasoLectura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapituloId = table.Column<int>(type: "int", nullable: false),
                    LecturaId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasoLectura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasoLectura_Capitulos_CapituloId",
                        column: x => x.CapituloId,
                        principalTable: "Capitulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PasoLectura_Lecturas_LecturaId",
                        column: x => x.LecturaId,
                        principalTable: "Lecturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasoLectura_CapituloId",
                table: "PasoLectura",
                column: "CapituloId");

            migrationBuilder.CreateIndex(
                name: "IX_PasoLectura_LecturaId",
                table: "PasoLectura",
                column: "LecturaId");
        }
    }
}
