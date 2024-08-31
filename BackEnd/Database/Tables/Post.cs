using System.ComponentModel.DataAnnotations;

namespace BackEnd.Database.Tables
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Images { get; set; }
        public string[] Videos { get; set; }
        public int NumLikes { get; set; }
        public int NumDislike { get; set; }
        public bool IsPublished { get; set; }
        public string ShareLink { get; set; }
        public string[] Hashtags { get; set; }
        public DateTime CreateAt { get; set; }

        public int? FK_BlogId { get; set; }
        public Blog? Blog { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
