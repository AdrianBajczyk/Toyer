using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class _22022024UserDevicesOneToManyDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevicesIds",
                table: "UsersDevices");

            migrationBuilder.AddColumn<string>(
                name: "UserDevicesUserId",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserDevicesUserId",
                table: "Devices",
                column: "UserDevicesUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_UsersDevices_UserDevicesUserId",
                table: "Devices",
                column: "UserDevicesUserId",
                principalTable: "UsersDevices",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_UsersDevices_UserDevicesUserId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserDevicesUserId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserDevicesUserId",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "DevicesIds",
                table: "UsersDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
