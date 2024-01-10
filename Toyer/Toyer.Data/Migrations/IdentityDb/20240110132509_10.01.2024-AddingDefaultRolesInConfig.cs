using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _10012024AddingDefaultRolesInConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c6b44db-a1b0-4f69-a677-4ad7d780253e", null, "Employee", "EMPLOYEE" },
                    { "72a9cb95-b813-4409-9bb0-62d11339a019", null, "RegisteredUser", "REGISTERED_USER" },
                    { "980f94aa-5d32-4b12-a5c5-547d938a93de", null, "Administrator", "ADMINISTRATOR" },
                    { "fb58c17a-6e7a-4e89-b7f6-857f2420cc1b", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6b44db-a1b0-4f69-a677-4ad7d780253e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72a9cb95-b813-4409-9bb0-62d11339a019");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "980f94aa-5d32-4b12-a5c5-547d938a93de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb58c17a-6e7a-4e89-b7f6-857f2420cc1b");
        }
    }
}
