using System.ComponentModel.DataAnnotations;

namespace ShareYourSword.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Likes { get; set; }

    }
}
