using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class settings01Characteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategories_tblCharacteristics_CharacteristicId",
                table: "tblCategories");

            migrationBuilder.DropIndex(
                name: "IX_tblCategories_CharacteristicId",
                table: "tblCategories");

            migrationBuilder.DropColumn(
                name: "CharacteristicId",
                table: "tblCategories");

            migrationBuilder.CreateTable(
                name: "CategoryEntityCharacteristicsEntity",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntityCharacteristicsEntity", x => new { x.CategoriesId, x.CharacteristicsId });
                    table.ForeignKey(
                        name: "FK_CategoryEntityCharacteristicsEntity_tblCategories_Categorie~",
                        column: x => x.CategoriesId,
                        principalTable: "tblCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEntityCharacteristicsEntity_tblCharacteristics_Char~",
                        column: x => x.CharacteristicsId,
                        principalTable: "tblCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityCharacteristicsEntity_CharacteristicsId",
                table: "CategoryEntityCharacteristicsEntity",
                column: "CharacteristicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEntityCharacteristicsEntity");

            migrationBuilder.AddColumn<int>(
                name: "CharacteristicId",
                table: "tblCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_CharacteristicId",
                table: "tblCategories",
                column: "CharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategories_tblCharacteristics_CharacteristicId",
                table: "tblCategories",
                column: "CharacteristicId",
                principalTable: "tblCharacteristics",
                principalColumn: "Id");
        }
    }
}
