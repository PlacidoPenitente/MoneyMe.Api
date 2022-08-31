using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class UpdatePrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("26e4541a-2ee6-4834-8f93-ed7ddfcffedb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50ffc053-1243-4aee-b5cc-5bf8f9946c85"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d9ad31d7-83c6-4085-a2d3-233037a27eb1"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Quotes_MonthlyAmotization",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Quotes_MonthlyAmotization",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Quotes",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Quotes",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Loans_Terms",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Loans_Terms",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Loans",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,4)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("dec7dae9-3bfd-4d33-bb96-7aadf03108e0"), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(2358), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(2373), 0m, 3, 1, "Product A", "InterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("2bab52b9-6ecc-49b1-8db8-c1bc9715c4a0"), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(5871), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(5875), 0.0949m, 12, 6, "Product B", "FirstTwoMonthsInterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("bfb6a1fb-94c6-4346-be58-96b58bcf1ce8"), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(5979), new DateTime(2022, 8, 31, 23, 49, 47, 76, DateTimeKind.Utc).AddTicks(5980), 0.0949m, 36, 18, "Product C", "NoInterestFreeRule" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2bab52b9-6ecc-49b1-8db8-c1bc9715c4a0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bfb6a1fb-94c6-4346-be58-96b58bcf1ce8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dec7dae9-3bfd-4d33-bb96-7aadf03108e0"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Quotes_MonthlyAmotization",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Quotes_MonthlyAmotization",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Quotes",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Quotes",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Loans_Terms",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Loans_Terms",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LoanAmount",
                table: "Loans",
                type: "decimal(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("26e4541a-2ee6-4834-8f93-ed7ddfcffedb"), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(3360), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(3380), 0m, 3, 1, "Product A", "InterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("d9ad31d7-83c6-4085-a2d3-233037a27eb1"), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9214), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9236), 0.0949m, 12, 6, "Product B", "FirstTwoMonthsInterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("50ffc053-1243-4aee-b5cc-5bf8f9946c85"), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9457), new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9459), 0.0949m, 36, 18, "Product C", "NoInterestFreeRule" });
        }
    }
}
