using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyMe.Infrastructure.Migrations
{
    public partial class RenamedFeeDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "quotes",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "products",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "payments",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "loans",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "fees",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "customers",
                newName: "DateCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "quotes",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "products",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "payments",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "loans",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "fees",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "customers",
                newName: "DateAdded");
        }
    }
}
