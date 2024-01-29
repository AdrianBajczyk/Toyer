using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Toyer.Data.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class _17012024AddingRefreshTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DevicesFKs",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ed89708-0cca-4cab-a687-704b80ffbc09", null, "Employee", "EMPLOYEE" },
                    { "9b6ba90b-67c7-4e97-81d7-590bbab41ac8", null, "Visitor", "VISITOR" },
                    { "a02df834-db84-4bca-a84d-09ef9285a1f7", null, "Administrator", "ADMINISTRATOR" },
                    { "b3be87de-4e11-4c78-bced-554e64143886", null, "RegisteredUser", "REGISTEREDUSER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ed89708-0cca-4cab-a687-704b80ffbc09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b6ba90b-67c7-4e97-81d7-590bbab41ac8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a02df834-db84-4bca-a84d-09ef9285a1f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3be87de-4e11-4c78-bced-554e64143886");

            migrationBuilder.AddColumn<string>(
                name: "DevicesFKs",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
