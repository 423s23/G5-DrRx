using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DxMood.Migrations
{
    /// <inheritdoc />
    public partial class removergeneratedresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultGenerated",
                table: "Results");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultGenerated",
                table: "Results",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
