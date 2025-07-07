using Food_Recipe.DAL.Data;
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Recipe.DAL.Repositories;

public class SavedRepository : ISavedRepository
{
    private readonly FoodRecipeDbContext _ctx;
    public SavedRepository(FoodRecipeDbContext ctx)
    {
        _ctx = ctx;
    }

    public Task<List<int>> GetSavedRecipeIdsAsync(string u)
        => _ctx.SavedRecipes
               .Where(s => s.Username == u)
               .Select(s => s.RecipeId)
               .ToListAsync();

    public async Task AddToSavedAsync(string u, int id)
    {
        if (!await IsSavedAsync(u, id))
        {
            _ctx.SavedRecipes.Add(new SavedRecipe
            {
                Username = u,
                RecipeId = id,
                SavedOn = DateTime.Now
            });
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveFromSavedAsync(string u, int id)
    {
        var s = await _ctx.SavedRecipes
                 .FirstOrDefaultAsync(x => x.Username == u && x.RecipeId == id);
        if (s != null) { _ctx.SavedRecipes.Remove(s); await _ctx.SaveChangesAsync(); }
    }

    public Task<bool> IsSavedAsync(string u, int id)
        => _ctx.SavedRecipes.AnyAsync(x => x.Username == u && x.RecipeId == id);
}
