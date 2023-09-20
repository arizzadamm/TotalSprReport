using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotalSprReport.Migrations
{
    /// <inheritdoc />
    public partial class SPRdbcontxt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SPRCode",
                table: "Header_SPR",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SPRCode",
                table: "Header_SPR");
        }
    }
}
