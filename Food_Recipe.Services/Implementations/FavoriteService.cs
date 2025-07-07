using Food_Recipe.DAL.Repositories;
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food_Recipe.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _FavoriteRepository;

        public FavoriteService(IFavoriteRepository FavoriteRepository)
        {
            _FavoriteRepository = FavoriteRepository;
        }

        public async Task<List<int>> GetFavoriteRecipeIdsAsync(string username)
        {
            return await _FavoriteRepository.GetFavoriteRecipeIdsAsync(username);
        }

        public async Task AddToFavoriteAsync(string username, int recipeId)
        {
            await _FavoriteRepository.AddToFavoriteAsync(username, recipeId);
        }

        public async Task RemoveFromFavoriteAsync(string username, int recipeId)
        {
            await _FavoriteRepository.RemoveFromFavoriteAsync(username, recipeId);
        }

        public async Task<bool> IsFavoriteAsync(string username, int recipeId)
        {
            return await _FavoriteRepository.IsFavoriteAsync(username, recipeId);
        }
    }
}
