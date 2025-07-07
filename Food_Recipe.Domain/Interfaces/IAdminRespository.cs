using Food_Recipe.Domain.Models;

namespace Food_Recipe.Domain.Interfaces;

public interface IAdminRepository
{
    List<User> GetAllUsers();
    List<PendingUserRecipe> GetPendingRecipes();
    List<UserRecipe> GetApprovedRecipes();
    PendingUserRecipe? GetPendingById(int id);
    void UpdatePending(PendingUserRecipe r);
    void DeletePending(int id);
    void AddApproved(UserRecipe recipe);
    void Save();
}
