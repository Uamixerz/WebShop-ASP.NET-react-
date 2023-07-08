using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class settingsProductforOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblProducts_ProductEntityId",
                table: "tblOrders");

            migrationBuilder.DropIndex(
                name: "IX_tblOrders_ProductEntityId",
                table: "tblOrders");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "tblOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                table: "tblOrders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_ProductEntityId",
                table: "tblOrders",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblProducts_ProductEntityId",
                table: "tblOrders",
                column: "ProductEntityId",
                principalTable: "tblProducts",
                principalColumn: "Id");
        }
    }
}
