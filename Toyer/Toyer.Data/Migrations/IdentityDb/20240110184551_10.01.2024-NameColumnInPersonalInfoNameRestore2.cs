using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _10012024NameColumnInPersonalInfoNameRestore2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "02e7d702-3bdf-4fa8-b17a-65670126b242", null, "Visitor", "VISITOR" },
                    { "6849dfe0-7ea9-4a6a-bdd1-7fd0fa008b22", null, "RegisteredUser", "REGISTEREDUSER" },
                    { "81822c2f-30b6-49e1-ad2d-1781e51bfa54", null, "Administrator", "ADMINISTRATOR" },
                    { "cc0e2755-9e5e-48a8-96cd-9ef7ba79ba29", null, "Employee", "EMPLOYEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PersonalInfos");

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
    }
}
