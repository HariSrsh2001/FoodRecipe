using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food_Recipe.Services.Implementations
{
    public class SavedService : ISavedService
    {
        private readonly ISavedRepository _savedRepository;

        public SavedService(ISavedRepository savedRepository)
        {
            _savedRepository = savedRepository;
        }

        public async Task<List<int>> GetSavedRecipeIdsAsync(string username)
        {
            return await _savedRepository.GetSavedRecipeIdsAsync(username);
        }

        public async Task AddToSavedAsync(string username, int recipeId)
        {
            await _savedRepository.AddToSavedAsync(username, recipeId);
        }

        public async Task RemoveFromSavedAsync(string username, int recipeId)
        {
            await _savedRepository.RemoveFromSavedAsync(username, recipeId);
        }

        public async Task<bool> IsSavedAsync(string username, int recipeId)
        {
            return await _savedRepository.IsSavedAsync(username, recipeId);
        }
    }
}
