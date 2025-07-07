// DAL/Repositories/UserRepository.cs
using Food_Recipe.DAL.Data;
using Food_Recipe.Domain.Interfaces;
using Food_Recipe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Food_Recipe.DAL.Repositories
{
    public class UserRepository(FoodRecipeDbContext context) : IUserRepository
    {
        private readonly FoodRecipeDbContext _context = context;

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> FindForLoginAsync(string input)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == input || u.Email == input);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        

    }
}
