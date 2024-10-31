using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFirstApplicationInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleTime",
                table: "FirstApplicationInfos");

            migrationBuilder.RenameColumn(
                name: "DateRegistered",
                table: "FirstApplicationInfos",
                newName: "TransactionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "FirstApplicationInfos",
                newName: "DateRegistered");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleTime",
                table: "FirstApplicationInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
