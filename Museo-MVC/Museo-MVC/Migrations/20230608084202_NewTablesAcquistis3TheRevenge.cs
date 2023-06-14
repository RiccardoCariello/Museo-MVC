using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museo_MVC.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesAcquistis3TheRevenge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acquistis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cap = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SouvenirId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acquistis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acquistis_Souvenirs_SouvenirId",
                        column: x => x.SouvenirId,
                        principalTable: "Souvenirs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acquistis_SouvenirId",
                table: "Acquistis",
                column: "SouvenirId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acquistis");
        }
    }
}
