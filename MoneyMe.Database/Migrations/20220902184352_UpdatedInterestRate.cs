using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class UpdatedInterestRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3641545b-90ae-4adf-9132-01967878db20"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("445d8075-9782-46c5-9e6c-5020f2ed05bb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d3cfa251-9e38-49f4-87a9-c33e57f9d96e"));

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("a42f8f96-340f-40e7-aeb8-d6e946219f84"), new DateTime(2022, 9, 2, 18, 43, 52, 158, DateTimeKind.Utc).AddTicks(6349), new DateTime(2022, 9, 2, 18, 43, 52, 158, DateTimeKind.Utc).AddTicks(6366), 0m, 5, 1, "Product A", "InterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("97638063-1911-4afa-9b62-4dbe94a0c7e3"), new DateTime(2022, 9, 2, 18, 43, 52, 159, DateTimeKind.Utc).AddTicks(161), new DateTime(2022, 9, 2, 18, 43, 52, 159, DateTimeKind.Utc).AddTicks(165), 0.0949m, 9, 6, "Product B", "FirstTwoMonthsInterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("af224dce-e165-49c4-aa0b-decf373f7d4f"), new DateTime(2022, 9, 2, 18, 43, 52, 159, DateTimeKind.Utc).AddTicks(281), new DateTime(2022, 9, 2, 18, 43, 52, 159, DateTimeKind.Utc).AddTicks(281), 0.0949m, 36, 10, "Product C", "NoInterestFreeRule" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("97638063-1911-4afa-9b62-4dbe94a0c7e3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a42f8f96-340f-40e7-aeb8-d6e946219f84"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("af224dce-e165-49c4-aa0b-decf373f7d4f"));

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("3641545b-90ae-4adf-9132-01967878db20"), new DateTime(2022, 9, 2, 18, 42, 16, 115, DateTimeKind.Utc).AddTicks(9196), new DateTime(2022, 9, 2, 18, 42, 16, 115, DateTimeKind.Utc).AddTicks(9211), 0m, 5, 1, "Product A", "InterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("d3cfa251-9e38-49f4-87a9-c33e57f9d96e"), new DateTime(2022, 9, 2, 18, 42, 16, 116, DateTimeKind.Utc).AddTicks(2903), new DateTime(2022, 9, 2, 18, 42, 16, 116, DateTimeKind.Utc).AddTicks(2907), 0.0949m, 9, 6, "Product B", "FirstTwoMonthsInterestFreeRule" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "MaximumDuration", "MinimumDuration", "Name", "Rule" },
                values: new object[] { new Guid("445d8075-9782-46c5-9e6c-5020f2ed05bb"), new DateTime(2022, 9, 2, 18, 42, 16, 116, DateTimeKind.Utc).AddTicks(3011), new DateTime(2022, 9, 2, 18, 42, 16, 116, DateTimeKind.Utc).AddTicks(3012), 0.0949m, 36, 10, "Product C", "NoInterestFreeRule" });
        }
    }
}
