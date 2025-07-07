using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Recipe.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedToPendingUserRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "PendingUserRecipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "PendingUserRecipes");
        }
    }
}
