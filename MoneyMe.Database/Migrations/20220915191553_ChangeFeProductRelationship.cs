using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class ChangeFeProductRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fees_products_ProductId",
                table: "fees");

            migrationBuilder.DropIndex(
                name: "IX_fees_ProductId",
                table: "fees");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "fees");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "fees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fees_ProductId",
                table: "fees",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_fees_products_ProductId",
                table: "fees",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
