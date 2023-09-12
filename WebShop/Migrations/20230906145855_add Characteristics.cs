using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebShop.Migrations
{
    /// <inheritdoc />
    public partial class addCharacteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacteristicId",
                table: "tblCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCharacteristicsProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCharacteristicsProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCharacteristicsProduct_tblCharacteristics_Characteristic~",
                        column: x => x.CharacteristicId,
                        principalTable: "tblCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCharacteristicsProduct_tblProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_CharacteristicId",
                table: "tblCategories",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacteristicsProduct_CharacteristicId",
                table: "tblCharacteristicsProduct",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCharacteristicsProduct_ProductId",
                table: "tblCharacteristicsProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategories_tblCharacteristics_CharacteristicId",
                table: "tblCategories",
                column: "CharacteristicId",
                principalTable: "tblCharacteristics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategories_tblCharacteristics_CharacteristicId",
                table: "tblCategories");

            migrationBuilder.DropTable(
                name: "tblCharacteristicsProduct");

            migrationBuilder.DropTable(
                name: "tblCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_tblCategories_CharacteristicId",
                table: "tblCategories");

            migrationBuilder.DropColumn(
                name: "CharacteristicId",
                table: "tblCategories");
        }
    }
}
