using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class settingsOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderEntityOrderItemEntity");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderItemEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_OrderId",
                table: "OrderItemEntity",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemEntity_tblOrders_OrderId",
                table: "OrderItemEntity",
                column: "OrderId",
                principalTable: "tblOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemEntity_tblOrders_OrderId",
                table: "OrderItemEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemEntity_OrderId",
                table: "OrderItemEntity");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderItemEntity");

            migrationBuilder.CreateTable(
                name: "OrderEntityOrderItemEntity",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "integer", nullable: false),
                    OrdersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntityOrderItemEntity", x => new { x.ItemsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_OrderEntityOrderItemEntity_OrderItemEntity_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "OrderItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderEntityOrderItemEntity_tblOrders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "tblOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntityOrderItemEntity_OrdersId",
                table: "OrderEntityOrderItemEntity",
                column: "OrdersId");
        }
    }
}
