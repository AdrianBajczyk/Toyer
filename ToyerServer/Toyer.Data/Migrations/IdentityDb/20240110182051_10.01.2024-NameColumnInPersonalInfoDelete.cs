using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _10012024NameColumnInPersonalInfoDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PersonalInfos");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PersonalInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
