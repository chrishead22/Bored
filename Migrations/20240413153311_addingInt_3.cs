using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bored.Migrations
{
    /// <inheritdoc />
    public partial class addingInt_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempted",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Activities");

            migrationBuilder.AddColumn<bool>(
                name: "Bad",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Good",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bad",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Good",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "Attempted",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Completed",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
