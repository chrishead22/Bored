using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bored.Migrations
{
    /// <inheritdoc />
    public partial class addingInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempted",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Activities");
        }
    }
}
