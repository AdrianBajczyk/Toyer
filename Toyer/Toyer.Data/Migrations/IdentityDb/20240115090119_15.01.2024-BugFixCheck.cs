using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _15012024BugFixCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "464b2855-417f-4c10-a108-21960b034da4", null, "Employee", "EMPLOYEE" },
                    { "651e5aa6-bf12-4a4b-9480-f6ad3a7d1165", null, "Visitor", "VISITOR" },
                    { "c65dc37e-eb94-4f31-9696-606ddbe5d275", null, "Administrator", "ADMINISTRATOR" },
                    { "d7e2ad8a-c921-494d-b019-33689696f871", null, "RegisteredUser", "REGISTEREDUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "464b2855-417f-4c10-a108-21960b034da4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651e5aa6-bf12-4a4b-9480-f6ad3a7d1165");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c65dc37e-eb94-4f31-9696-606ddbe5d275");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7e2ad8a-c921-494d-b019-33689696f871");

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
    }
}
