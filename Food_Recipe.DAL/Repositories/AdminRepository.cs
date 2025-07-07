using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Food_Recipe.DAL.Data;
using System.Linq;

namespace Food_Recipe.DAL.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly FoodRecipeDbContext _ctx;
    public AdminRepository(FoodRecipeDbContext ctx) => _ctx = ctx;

    public List<User> GetAllUsers() => _ctx.Users.ToList();

    public List<PendingUserRecipe> GetPendingRecipes() =>
        _ctx.PendingUserRecipes.Where(x => !x.IsApproved && !x.IsRejected).ToList();

    public List<UserRecipe> GetApprovedRecipes() =>
        _ctx.UserRecipes.OrderByDescending(r => r.CreatedAt).ToList();

    public PendingUserRecipe? GetPendingById(int id) =>
        _ctx.PendingUserRecipes.FirstOrDefault(x => x.Id == id);

    public void UpdatePending(PendingUserRecipe r)
    {
        _ctx.PendingUserRecipes.Update(r);
    }

    public void DeletePending(int id)
    {
        var p = GetPendingById(id);
        if (p != null) _ctx.PendingUserRecipes.Remove(p);
    }

    public void AddApproved(UserRecipe recipe)
    {
        _ctx.UserRecipes.Add(recipe);
    }

    public void Save() => _ctx.SaveChanges();
}
