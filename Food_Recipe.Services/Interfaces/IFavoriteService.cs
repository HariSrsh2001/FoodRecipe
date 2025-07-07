namespace Food_Recipe.Domain.Interfaces;

public interface IFavoriteService
{
    //Task ToggleFavoriteAsync(string username, int recipeId);

    Task<List<int>> GetFavoriteRecipeIdsAsync(string username);
    Task AddToFavoriteAsync(string username, int recipeId);
    Task RemoveFromFavoriteAsync(string username, int recipeId);
    Task<bool> IsFavoriteAsync(string username, int recipeId);
}
