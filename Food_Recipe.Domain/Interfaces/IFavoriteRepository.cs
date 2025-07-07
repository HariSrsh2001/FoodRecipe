namespace Food_Recipe.Domain.Interfaces;

public interface IFavoriteRepository
{
    Task<List<int>> GetFavoriteRecipeIdsAsync(string username);
    Task AddToFavoriteAsync(string username, int recipeId);
    Task RemoveFromFavoriteAsync(string username, int recipeId);
    Task<bool> IsFavoriteAsync(string username, int recipeId);
}
