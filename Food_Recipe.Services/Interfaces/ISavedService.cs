using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food_Recipe.Services.Interfaces
{
    public interface ISavedService
    {
        Task<List<int>> GetSavedRecipeIdsAsync(string username);
        Task AddToSavedAsync(string username, int recipeId);
        Task RemoveFromSavedAsync(string username, int recipeId);
        Task<bool> IsSavedAsync(string username, int recipeId);
    }
}
