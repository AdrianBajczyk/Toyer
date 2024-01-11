using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _10012024NameColumnInPersonalInfoNameRestore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27ad2152-419f-4af7-acc7-3353fd24a0ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "424fb0bd-6417-4c3f-9173-bee966545409");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7821103d-a6f4-4a8b-b3fb-95ad8f2cae9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "993af569-5ede-4902-9f5d-229ddfa7e3f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "316342d3-74a5-422d-8fd9-8179bf944c5e", null, "Employee", "EMPLOYEE" },
                    { "3f614e4d-bac1-4c10-8139-5f2ada079831", null, "Visitor", "VISITOR" },
                    { "8ee032ed-8059-4197-918f-0b3b33afb1a4", null, "Administrator", "ADMINISTRATOR" },
                    { "bca93ec7-e318-4e7b-9da7-be516ef69cb8", null, "RegisteredUser", "REGISTEREDUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "316342d3-74a5-422d-8fd9-8179bf944c5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f614e4d-bac1-4c10-8139-5f2ada079831");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ee032ed-8059-4197-918f-0b3b33afb1a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bca93ec7-e318-4e7b-9da7-be516ef69cb8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27ad2152-419f-4af7-acc7-3353fd24a0ba", null, "Employee", "EMPLOYEE" },
                    { "424fb0bd-6417-4c3f-9173-bee966545409", null, "Administrator", "ADMINISTRATOR" },
                    { "7821103d-a6f4-4a8b-b3fb-95ad8f2cae9e", null, "Visitor", "VISITOR" },
                    { "993af569-5ede-4902-9f5d-229ddfa7e3f2", null, "RegisteredUser", "REGISTEREDUSER" }
                });
        }
    }
}
