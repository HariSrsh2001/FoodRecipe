using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Food_Recipe.Domain.Models
{
    public class RecipeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Name { get; set; }

        public string Img { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Nutrition { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }

        //public string Username { get; set; }  // 👤 Owner
    }
}