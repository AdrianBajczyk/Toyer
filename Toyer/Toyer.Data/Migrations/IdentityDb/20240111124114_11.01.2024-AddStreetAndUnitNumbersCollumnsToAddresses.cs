using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _11012024AddStreetAndUnitNumbersCollumnsToAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02e7d702-3bdf-4fa8-b17a-65670126b242");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6849dfe0-7ea9-4a6a-bdd1-7fd0fa008b22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81822c2f-30b6-49e1-ad2d-1781e51bfa54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc0e2755-9e5e-48a8-96cd-9ef7ba79ba29");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02e7d702-3bdf-4fa8-b17a-65670126b242", null, "Visitor", "VISITOR" },
                    { "6849dfe0-7ea9-4a6a-bdd1-7fd0fa008b22", null, "RegisteredUser", "REGISTEREDUSER" },
                    { "81822c2f-30b6-49e1-ad2d-1781e51bfa54", null, "Administrator", "ADMINISTRATOR" },
                    { "cc0e2755-9e5e-48a8-96cd-9ef7ba79ba29", null, "Employee", "EMPLOYEE" }
                });
        }
    }
}
