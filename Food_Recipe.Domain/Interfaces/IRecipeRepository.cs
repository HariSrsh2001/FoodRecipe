// File: Food_Recipe.Domain.Interfaces/IRecipeRepository.cs
using Food_Recipe.Domain.Models;

public interface IRecipeRepository
{
    /* FoodRecipes table */
    IQueryable<RecipeEntity> FoodRecipes();           // returns an IQueryable for filtering
    RecipeEntity? GetFoodById(int id);

    /* Favourites & Saved */
    IEnumerable<int> GetFavoriteIds(string user);
    IEnumerable<int> GetSavedIds(string user);
    void AddFavorite(string user, int id);
    void AddSaved(string user, int id);

    /* PendingUserRecipes */
    IEnumerable<PendingUserRecipe> GetPending(string user);
    PendingUserRecipe? GetPendingById(int id);
    void AddPending(PendingUserRecipe r);
    void UpdatePending(PendingUserRecipe r);
    void DeletePending(int id, string user);
    List<UserRecipe> GetApprovedRecipesByUser(string username);
}
