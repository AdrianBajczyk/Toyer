using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class _14022024ImgUrlToDeviceTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DeviceTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DeviceTypes");
        }
    }
}
