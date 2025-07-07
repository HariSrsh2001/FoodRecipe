// Domain/Interfaces/IUserRepository.cs
using Food_Recipe.Domain.Models;
using System.Threading.Tasks;

namespace Food_Recipe.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username, string email);
        Task AddAsync(User user);
        Task<User?> FindForLoginAsync(string input); // username or email
        Task<User?> GetByEmailAsync(string email);
        

    }
}
