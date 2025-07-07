// File: Food_Recipe.DAL.Repositories/RecipeRepository.cs
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Food_Recipe.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class RecipeRepository : IRecipeRepository
{
    private readonly FoodRecipeDbContext _ctx;
    public RecipeRepository(FoodRecipeDbContext ctx) => _ctx = ctx;

    /* -------- FoodRecipes -------- */
    public IQueryable<RecipeEntity> FoodRecipes() => _ctx.FoodRecipes.AsQueryable();
    public RecipeEntity? GetFoodById(int id) => _ctx.FoodRecipes.Find(id);

    /* -------- Favourites / Saved -------- */
    public IEnumerable<int> GetFavoriteIds(string u) =>
        _ctx.FavoriteRecipes.Where(f => f.Username == u).Select(f => f.RecipeId).ToList();

    public IEnumerable<int> GetSavedIds(string u) =>
        _ctx.SavedRecipes.Where(s => s.Username == u).Select(s => s.RecipeId).ToList();

    public void AddFavorite(string u, int id)
    {
        if (!_ctx.FavoriteRecipes.Any(f => f.Username == u && f.RecipeId == id))
        {
            _ctx.FavoriteRecipes.Add(new FavoriteRecipe { Username = u, RecipeId = id, FavoritedAt = DateTime.Now });
            _ctx.SaveChanges();
        }
    }
    public void AddSaved(string u, int id)
    {
        if (!_ctx.SavedRecipes.Any(s => s.Username == u && s.RecipeId == id))
        {
            _ctx.SavedRecipes.Add(new SavedRecipe { Username = u, RecipeId = id, SavedOn = DateTime.Now });
            _ctx.SaveChanges();
        }
    }

    /* -------- PendingUserRecipes -------- */
    public IEnumerable<PendingUserRecipe> GetPending(string u) =>
        _ctx.PendingUserRecipes.Where(p => p.Username == u)
                               .OrderByDescending(p => p.CreatedAt).ToList();

    public PendingUserRecipe? GetPendingById(int id) =>
        _ctx.PendingUserRecipes.FirstOrDefault(p => p.Id == id);

    public List<UserRecipe> GetApprovedRecipesByUser(string username)
    {
        return _ctx.UserRecipes
                   .Where(r => r.Username == username)
                   .OrderByDescending(r => r.CreatedAt)
                   .ToList();
    }

    public void AddPending(PendingUserRecipe r) { _ctx.PendingUserRecipes.Add(r); _ctx.SaveChanges(); }
    public void UpdatePending(PendingUserRecipe r) { _ctx.PendingUserRecipes.Update(r); _ctx.SaveChanges(); }
    public void DeletePending(int id, string u)
    {
        var p = _ctx.PendingUserRecipes.FirstOrDefault(x => x.Id == id && x.Username == u && !x.IsApproved);
        if (p != null) { _ctx.PendingUserRecipes.Remove(p); _ctx.SaveChanges(); }
    }
}
