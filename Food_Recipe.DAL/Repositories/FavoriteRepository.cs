using Food_Recipe.DAL.Data;
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Recipe.DAL.Repositories;

public class FavoriteRepository(FoodRecipeDbContext ctx) : IFavoriteRepository
{
    private readonly FoodRecipeDbContext _ctx = ctx;

    public Task<List<int>> GetFavoriteRecipeIdsAsync(string u)
        => _ctx.FavoriteRecipes
               .Where(f => f.Username == u)
               .Select(f => f.RecipeId)
               .ToListAsync();

    public async Task AddToFavoriteAsync(string u, int id)
    {
        if (!await IsFavoriteAsync(u, id))
        {
            _ctx.FavoriteRecipes.Add(new FavoriteRecipe
            {
                Username = u,
                RecipeId = id,
                FavoritedAt = DateTime.Now
            });
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveFromFavoriteAsync(string u, int id)
    {
        var f = await _ctx.FavoriteRecipes
                 .FirstOrDefaultAsync(x => x.Username == u && x.RecipeId == id);
        if (f != null) { _ctx.FavoriteRecipes.Remove(f); await _ctx.SaveChangesAsync(); }
    }

    public Task<bool> IsFavoriteAsync(string u, int id)
        => _ctx.FavoriteRecipes.AnyAsync(x => x.Username == u && x.RecipeId == id);
}
