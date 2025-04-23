using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fullstack_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpotter",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrainer",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserBio",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLocation",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UserLocationPublic",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserPrimarySport",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSecondarySport",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpotter",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsTrainer",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserBio",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserLocation",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserLocationPublic",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserPrimarySport",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserSecondarySport",
                table: "User");
        }
    }
}
