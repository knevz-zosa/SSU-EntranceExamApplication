using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeniorAwardNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeniorAwards",
                table: "AcademicBackgrounds");

            migrationBuilder.AddColumn<string>(
                name: "SeniorAward",
                table: "AcademicBackgrounds",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeniorAward",
                table: "AcademicBackgrounds");

            migrationBuilder.AddColumn<string>(
                name: "SeniorAwards",
                table: "AcademicBackgrounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
