using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEnd.Database.Tables
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; }
        public List<string> Videos { get; set; }
        public int NumLikes { get; set; }
        public int NumDislike { get; set; }
        public bool IsPublished { get; set; }
        public List<string> Hashtags { get; set; }
        public DateTime CreateAt { get; set; }
        public int? FK_BlogId { get; set; }
        [JsonIgnore]
        public Blog? Blog { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
