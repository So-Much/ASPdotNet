using System.ComponentModel.DataAnnotations;

namespace BackEnd.Database.Tables
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public int NumLikes { get; set; }
        public int NumDislike { get; set; }
        public int FK_PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int FK_UserId { get; set; }
        public User Author { get; set; } = null!;
    }
}
