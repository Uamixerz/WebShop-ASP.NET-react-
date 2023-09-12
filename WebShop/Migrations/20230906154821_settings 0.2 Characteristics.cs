using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class settings02Characteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEntityCharacteristicsEntity");

            migrationBuilder.CreateTable(
                name: "tblCharacteristicsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCharacteristicsCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCharacteristicsCategory_tblCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCharacteristicsCategory_tblCharacteristics_Characteristi~",
                        column: x => x.CharacteristicId,
                        principalTable: "tblCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacteristicsCategory_CategoryId",
                table: "tblCharacteristicsCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacteristicsCategory_CharacteristicId",
                table: "tblCharacteristicsCategory",
                column: "CharacteristicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCharacteristicsCategory");

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
    }
}
