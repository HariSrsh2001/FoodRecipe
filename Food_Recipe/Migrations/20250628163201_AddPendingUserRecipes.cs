using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Recipe.Migrations
{
    public partial class AddPendingUserRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ❌ Don't drop a table that may not exist
            // migrationBuilder.DropTable(name: "MyRecipes");

            migrationBuilder.CreateTable(
                name: "PendingUserRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingUserRecipes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingUserRecipes");

            // Optional: Restore MyRecipes if needed
            // migrationBuilder.CreateTable(
            //     name: "MyRecipes",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         RecipeId = table.Column<int>(type: "int", nullable: false),
            //         Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_MyRecipes", x => x.Id);
            //     });
        }
    }
}
