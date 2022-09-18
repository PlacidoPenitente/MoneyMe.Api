using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class MovedFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fees_quotes_QuoteId",
                table: "fees");

            migrationBuilder.RenameColumn(
                name: "QuoteId",
                table: "fees",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_fees_QuoteId",
                table: "fees",
                newName: "IX_fees_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_fees_products_ProductId",
                table: "fees",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fees_products_ProductId",
                table: "fees");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "fees",
                newName: "QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_fees_ProductId",
                table: "fees",
                newName: "IX_fees_QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_fees_quotes_QuoteId",
                table: "fees",
                column: "QuoteId",
                principalTable: "quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
