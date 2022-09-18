using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class FixedProductAndFeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeProduct");

            migrationBuilder.CreateTable(
                name: "product_fee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_fee_fees_FeeId",
                        column: x => x.FeeId,
                        principalTable: "fees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_fee_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_fee_FeeId",
                table: "product_fee",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_fee_ProductId",
                table: "product_fee",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_fee");

            migrationBuilder.CreateTable(
                name: "FeeProduct",
                columns: table => new
                {
                    FeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeProduct", x => new { x.FeesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_FeeProduct_fees_FeesId",
                        column: x => x.FeesId,
                        principalTable: "fees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeeProduct_products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeProduct_ProductsId",
                table: "FeeProduct",
                column: "ProductsId");
        }
    }
}
