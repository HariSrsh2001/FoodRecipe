using Food_Recipe.Domain.Models; // Ensure this contains your models like AppUser
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Food_Recipe.DAL.Data
{
    public class FoodRecipeDbContext : DbContext
    {
        public FoodRecipeDbContext(DbContextOptions<FoodRecipeDbContext> options) : base(options) { }

        //public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RecipeEntity> FoodRecipes { get; set; }
        public DbSet<PendingUserRecipe> PendingUserRecipes { get; set; }
        public DbSet<UserRecipe> UserRecipes { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<SavedRecipe> SavedRecipes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //mb.Entity<AppUser>().ToTable("Users");
            mb.Entity<User>().ToTable("Users");

            mb.Entity<RecipeEntity>().ToTable("FoodRecipes");
            mb.Entity<PendingUserRecipe>().ToTable("PendingUserRecipes");
            mb.Entity<UserRecipe>().ToTable("UserRecipes");
            mb.Entity<FavoriteRecipe>().ToTable("FavoriteRecipes");
            mb.Entity<SavedRecipe>().ToTable("SavedRecipes");
        }
    }
}
