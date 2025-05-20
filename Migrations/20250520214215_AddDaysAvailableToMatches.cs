using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fullstack_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDaysAvailableToMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Matches",
                newName: "DaysAvailable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DaysAvailable",
                table: "Matches",
                newName: "DateCreated");
        }
    }
}
