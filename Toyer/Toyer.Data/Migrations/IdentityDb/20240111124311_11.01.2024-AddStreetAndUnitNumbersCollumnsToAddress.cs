using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _11012024AddStreetAndUnitNumbersCollumnsToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d534034-598b-4c4d-8aa1-8ad90176352b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5aa52265-8695-492d-817f-d958fa91925c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "678021dd-4e87-4670-9aa8-58a7ee636b36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b3cd2e7-61d9-4a6a-b815-cc46269abf2f");

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitNumber",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6fd98fa6-0f3b-4681-86ba-5f9960626c01", null, "Administrator", "ADMINISTRATOR" },
                    { "7acb8de1-609c-4330-9f42-1a4220dc0030", null, "Employee", "EMPLOYEE" },
                    { "8106e8a8-9bf1-4cff-bef5-6dbb128cce01", null, "RegisteredUser", "REGISTEREDUSER" },
                    { "d5c2bc5b-7b3a-44e8-b4b3-0481ebb33cf3", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fd98fa6-0f3b-4681-86ba-5f9960626c01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7acb8de1-609c-4330-9f42-1a4220dc0030");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8106e8a8-9bf1-4cff-bef5-6dbb128cce01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5c2bc5b-7b3a-44e8-b4b3-0481ebb33cf3");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d534034-598b-4c4d-8aa1-8ad90176352b", null, "RegisteredUser", "REGISTEREDUSER" },
                    { "5aa52265-8695-492d-817f-d958fa91925c", null, "Employee", "EMPLOYEE" },
                    { "678021dd-4e87-4670-9aa8-58a7ee636b36", null, "Administrator", "ADMINISTRATOR" },
                    { "8b3cd2e7-61d9-4a6a-b815-cc46269abf2f", null, "Visitor", "VISITOR" }
                });
        }
    }
}
