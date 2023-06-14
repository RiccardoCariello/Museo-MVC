using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museo_MVC.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedAcquistisUmberto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Acquistis",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Acquistis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Acquistis");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Acquistis");
        }
    }
}
