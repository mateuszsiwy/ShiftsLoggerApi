using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftsLoggerApi.Migrations
{
    /// <inheritdoc />
    public partial class addedDurationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "ShiftItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ShiftItems");
        }
    }
}
