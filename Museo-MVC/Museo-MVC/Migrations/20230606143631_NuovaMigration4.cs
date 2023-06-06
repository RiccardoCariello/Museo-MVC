using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museo_MVC.Migrations
{
    /// <inheritdoc />
    public partial class NuovaMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Souvenirs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Souvenirs",
                type: "int",
                nullable: true);
        }
    }
}
