using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class minusMethodDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrders_tblDelivery_DeliveryMethodId",
                table: "tblOrders");

            migrationBuilder.DropIndex(
                name: "IX_tblOrders_DeliveryMethodId",
                table: "tblOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryMethodId",
                table: "tblOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethodId",
                table: "tblOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_DeliveryMethodId",
                table: "tblOrders",
                column: "DeliveryMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrders_tblDelivery_DeliveryMethodId",
                table: "tblOrders",
                column: "DeliveryMethodId",
                principalTable: "tblDelivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
