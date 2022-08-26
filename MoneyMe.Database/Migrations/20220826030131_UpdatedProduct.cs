using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class UpdatedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("93e12c92-a5e9-4b24-845c-f8c8301d1053"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a3dd17d8-26fa-43e9-8bf9-7e7896c135e4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aefefad7-ff28-4f28-b00e-a0f6b0c4a3ed"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e4c9fdfc-6c82-4c94-8bc9-7b2b7da09643"));

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Quotes",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Loans",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "Name", "Terms" },
                values: new object[,]
                {
                    { new Guid("3327bc7f-9afd-4252-8892-0248f7df8909"), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(4627), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(4642), 0m, "Product A", 3 },
                    { new Guid("01ffa25b-bade-4080-8282-9b2a07bf2253"), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6783), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6786), 0.0949m, "Product B", 6 },
                    { new Guid("ec3e6de4-a5b0-422d-b4af-64be6797c17a"), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6857), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6858), 0.0949m, "Product C", 12 },
                    { new Guid("038e54ad-cde4-4e87-a758-06f3e6c58afd"), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6860), new DateTime(2022, 8, 26, 3, 1, 31, 90, DateTimeKind.Utc).AddTicks(6860), 0.0949m, "Product D", 24 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("01ffa25b-bade-4080-8282-9b2a07bf2253"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("038e54ad-cde4-4e87-a758-06f3e6c58afd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3327bc7f-9afd-4252-8892-0248f7df8909"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ec3e6de4-a5b0-422d-b4af-64be6797c17a"));

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Quotes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "Name", "Terms" },
                values: new object[,]
                {
                    { new Guid("a3dd17d8-26fa-43e9-8bf9-7e7896c135e4"), new DateTime(2022, 8, 26, 2, 57, 35, 361, DateTimeKind.Utc).AddTicks(8093), new DateTime(2022, 8, 26, 2, 57, 35, 361, DateTimeKind.Utc).AddTicks(8104), 0m, "Product A", 3 },
                    { new Guid("aefefad7-ff28-4f28-b00e-a0f6b0c4a3ed"), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(164), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(168), 0.0949m, "Product B", 6 },
                    { new Guid("e4c9fdfc-6c82-4c94-8bc9-7b2b7da09643"), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(243), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(244), 0.0949m, "Product C", 12 },
                    { new Guid("93e12c92-a5e9-4b24-845c-f8c8301d1053"), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(246), new DateTime(2022, 8, 26, 2, 57, 35, 362, DateTimeKind.Utc).AddTicks(247), 0.0949m, "Product D", 24 }
                });
        }
    }
}
