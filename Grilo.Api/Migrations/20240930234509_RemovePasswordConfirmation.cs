using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grilo.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePasswordConfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordConfirmation",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordConfirmation",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
