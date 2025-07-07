using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Food_Recipe.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Food_Recipe.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repo;

        public RecipeService(IRecipeRepository repo) => _repo = repo;

        /* ------------ Catalog ------------ */
        public List<Recipe> FilterRecipes(string search, string category, int minRating)
        {
            var query = _repo.FoodRecipes();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Name.Contains(search) || r.Description.Contains(search));

            if (!string.IsNullOrWhiteSpace(category) && category != "All")
                query = query.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (minRating > 0)
                query = query.Where(r => r.Rating >= minRating);

            return query.ToList().Select(Convert).ToList();
        }

        //public List<Recipe> GetByCategory(string category) =>
        //    _repo.FoodRecipes()
        //         .Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
        //         .ToList()
        //         .Select(Convert)
        //         .ToList();
        public List<Recipe> GetByCategory(string category) =>
    _repo.FoodRecipes()
         .Where(r => r.Category.ToLower() == category.ToLower())
         .Select(Convert)
         .ToList();


        /* ------------ Favourites & Saved ------------ */
        public List<Recipe> GetFavoritesByUsername(string username) =>
            _repo.FoodRecipes()
                 .Where(r => _repo.GetFavoriteIds(username).Contains(r.Id))
                 .ToList()
                 .Select(Convert)
                 .ToList();

        public List<Recipe> GetSavedByUsername(string username) =>
            _repo.FoodRecipes()
                 .Where(r => _repo.GetSavedIds(username).Contains(r.Id))
                 .ToList()
                 .Select(Convert)
                 .ToList();

        public void MarkFavorite(int recipeId, string username) => _repo.AddFavorite(username, recipeId);
        public void MarkSaved(int recipeId, string username) => _repo.AddSaved(username, recipeId);

        /* ------------ Pending workflow ------------ */
        public List<PendingUserRecipe> GetPendingRecipesByUser(string username) =>
            _repo.GetPending(username).ToList();

        public PendingUserRecipe? GetPendingRecipeById(int id) => _repo.GetPendingById(id);

        public List<UserRecipe> GetApprovedRecipesByUser(string username)
        {
            return _repo.GetApprovedRecipesByUser(username);
        }


        public void SubmitUserRecipe(PendingUserRecipe recipe) => _repo.AddPending(recipe);
        public void UpdatePendingRecipe(PendingUserRecipe recipe) => _repo.UpdatePending(recipe);
        public void DeleteUserPendingRecipe(int id, string user) => _repo.DeletePending(id, user);

        public Dictionary<int, string> GetRecipeStatuses(IEnumerable<PendingUserRecipe> list) =>
            list.ToDictionary(
                x => x.Id,
                x => x.IsApproved ? "Approved"
                     : x.IsRejected ? "Rejected"
                     : "Pending");

        /* ------------ Util ------------ */
        public string SaveImage(IFormFile? file, string webRootPath)
        {
            if (file == null || file.Length == 0)
                return "/images/default.jpg";

            string uploads = Path.Combine(webRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"/uploads/{fileName}";
        }

        /* ------------ Mapping ------------ */
        private static Recipe Convert(RecipeEntity r) => new()
        {
            Id = r.Id,
            Name = r.Name,
            Category = r.Category,
            Description = r.Description,
            Img = r.Img,
            Rating = r.Rating,
            Ingredients = JsonConvert.DeserializeObject<List<string>>(r.Ingredients ?? "[]"),
            Instructions = JsonConvert.DeserializeObject<List<string>>(r.Instructions ?? "[]"),
            Nutrition = JsonConvert.DeserializeObject<Nutrition>(r.Nutrition ?? "{}")
        };
    }
}
