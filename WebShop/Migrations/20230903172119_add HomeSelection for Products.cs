using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class addHomeSelectionforProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomePagePriority",
                table: "tblProducts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HomePageSelection",
                table: "tblProducts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePagePriority",
                table: "tblProducts");

            migrationBuilder.DropColumn(
                name: "HomePageSelection",
                table: "tblProducts");
        }
    }
}
