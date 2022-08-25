using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "Name", "Terms" },
                values: new object[,]
                {
                    { new Guid("ba80d770-431b-40ce-bf77-9590d8b3d640"), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(7398), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(7411), 0m, "Product A", 3 },
                    { new Guid("00b9e97a-ce9e-454e-abb3-61bb0311ccf9"), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9575), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9578), 0.0949m, "Product B", 6 },
                    { new Guid("9bdc582f-6b0f-46a2-a320-2c2e4da0bcfa"), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9655), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9656), 0.0949m, "Product C", 12 },
                    { new Guid("7fce8b99-0d29-4955-a730-daaadadc0e71"), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9658), new DateTime(2022, 8, 25, 19, 0, 1, 155, DateTimeKind.Utc).AddTicks(9659), 0.0949m, "Product D", 24 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00b9e97a-ce9e-454e-abb3-61bb0311ccf9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7fce8b99-0d29-4955-a730-daaadadc0e71"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9bdc582f-6b0f-46a2-a320-2c2e4da0bcfa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ba80d770-431b-40ce-bf77-9590d8b3d640"));
        }
    }
}
