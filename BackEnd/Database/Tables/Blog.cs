using System.ComponentModel.DataAnnotations;

namespace BackEnd.Database.Tables
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsPublished { get; set; }
        public int? FK_UserId { get; set; }
        public User? Author { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
