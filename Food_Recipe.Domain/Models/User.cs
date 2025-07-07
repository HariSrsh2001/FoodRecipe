// Food_Recipe.Domain.Models/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Recipe.Domain.Models
{
    //[Table("Users")] // 🔑 Ensure correct table name
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Add other properties ONLY if they exist in the DB
    }
}
