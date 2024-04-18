using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTP_server.Migrations
{
    /// <inheritdoc />
    public partial class changedusertoapropriatename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "OneTimePasswords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OneTimePasswords",
                table: "OneTimePasswords",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OneTimePasswords",
                table: "OneTimePasswords");

            migrationBuilder.RenameTable(
                name: "OneTimePasswords",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
