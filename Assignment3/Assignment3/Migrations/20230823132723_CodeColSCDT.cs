using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class CodeColSCDT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SCode",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DTCode",
                table: "DayTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SCode",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DTCode",
                table: "DayTimes");
        }
    }
}
