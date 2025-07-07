namespace Food_Recipe.Domain.Interfaces;

public interface ISavedRepository
{
    Task<List<int>> GetSavedRecipeIdsAsync(string username);
  
    Task<bool> IsSavedAsync(string username, int recipeId);
    Task AddToSavedAsync(string username, int recipeId);
    Task RemoveFromSavedAsync(string username, int recipeId);
}
