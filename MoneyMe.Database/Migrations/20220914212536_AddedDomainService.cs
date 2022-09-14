using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class AddedDomainService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "quotes");

            migrationBuilder.DropColumn(
                name: "Rule",
                table: "products");

            migrationBuilder.AddColumn<Guid>(
                name: "RuleId",
                table: "products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuoteId",
                table: "fees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fees_QuoteId",
                table: "fees",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_fees_quotes_QuoteId",
                table: "fees",
                column: "QuoteId",
                principalTable: "quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fees_quotes_QuoteId",
                table: "fees");

            migrationBuilder.DropIndex(
                name: "IX_fees_QuoteId",
                table: "fees");

            migrationBuilder.DropColumn(
                name: "RuleId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "fees");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "quotes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Rule",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
