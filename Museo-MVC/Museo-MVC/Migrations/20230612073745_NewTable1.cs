using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museo_MVC.Migrations
{
    /// <inheritdoc />
    public partial class NewTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SouvenirId",
                table: "Ordini",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_SouvenirId",
                table: "Ordini",
                column: "SouvenirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Souvenirs_SouvenirId",
                table: "Ordini",
                column: "SouvenirId",
                principalTable: "Souvenirs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Souvenirs_SouvenirId",
                table: "Ordini");

            migrationBuilder.DropIndex(
                name: "IX_Ordini_SouvenirId",
                table: "Ordini");

            migrationBuilder.AlterColumn<int>(
                name: "SouvenirId",
                table: "Ordini",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
