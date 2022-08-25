using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class UpdatedDecimalPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0ad935c9-be78-4e0d-8508-d6e1311b634d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("29d4fb76-5fef-4433-ab43-9d418a71736d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6725bcd0-00e2-4669-8e80-5366e0c8fdc6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cc6a882e-eac2-420b-8cc2-35d7cf9dd59b"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "DateModified", "InterestRate", "Name", "Terms" },
                values: new object[,]
                {
                    { new Guid("0ad935c9-be78-4e0d-8508-d6e1311b634d"), new DateTime(2022, 8, 25, 19, 5, 18, 164, DateTimeKind.Utc).AddTicks(8077), new DateTime(2022, 8, 25, 19, 5, 18, 164, DateTimeKind.Utc).AddTicks(8098), 0m, "Product A", 3 },
                    { new Guid("6725bcd0-00e2-4669-8e80-5366e0c8fdc6"), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(615), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(619), 0.0949m, "Product B", 6 },
                    { new Guid("cc6a882e-eac2-420b-8cc2-35d7cf9dd59b"), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(700), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(701), 0.0949m, "Product C", 12 },
                    { new Guid("29d4fb76-5fef-4433-ab43-9d418a71736d"), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(703), new DateTime(2022, 8, 25, 19, 5, 18, 165, DateTimeKind.Utc).AddTicks(703), 0.0949m, "Product D", 24 }
                });
        }
    }
}
