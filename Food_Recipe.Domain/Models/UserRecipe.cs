namespace Food_Recipe.Domain.Models
{
    public class UserRecipe
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Rating { get; set; }

        public string Ingredients { get; set; }
        public string Instructions { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
