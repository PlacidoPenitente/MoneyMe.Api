using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class RenamedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_fee_fees_FeeId",
                table: "product_fee");

            migrationBuilder.DropForeignKey(
                name: "FK_product_fee_products_ProductId",
                table: "product_fee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_fee",
                table: "product_fee");

            migrationBuilder.RenameTable(
                name: "product_fee",
                newName: "product_fees");

            migrationBuilder.RenameIndex(
                name: "IX_product_fee_ProductId",
                table: "product_fees",
                newName: "IX_product_fees_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_product_fee_FeeId",
                table: "product_fees",
                newName: "IX_product_fees_FeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_fees",
                table: "product_fees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_fees_fees_FeeId",
                table: "product_fees",
                column: "FeeId",
                principalTable: "fees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_fees_products_ProductId",
                table: "product_fees",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_fees_fees_FeeId",
                table: "product_fees");

            migrationBuilder.DropForeignKey(
                name: "FK_product_fees_products_ProductId",
                table: "product_fees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_fees",
                table: "product_fees");

            migrationBuilder.RenameTable(
                name: "product_fees",
                newName: "product_fee");

            migrationBuilder.RenameIndex(
                name: "IX_product_fees_ProductId",
                table: "product_fee",
                newName: "IX_product_fee_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_product_fees_FeeId",
                table: "product_fee",
                newName: "IX_product_fee_FeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_fee",
                table: "product_fee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_fee_fees_FeeId",
                table: "product_fee",
                column: "FeeId",
                principalTable: "fees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_fee_products_ProductId",
                table: "product_fee",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
