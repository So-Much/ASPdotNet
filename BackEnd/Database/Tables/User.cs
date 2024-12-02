using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BackEnd.Database.Tables
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; } = "";
        public string UID { get; set; } = "";
        [StringLength(255)]
        public string Name { get; set; } = "";
        [StringLength(255)]
        public string Email { get; set; } = "";
        [StringLength(255)]
        public string Password { get; set; } = "";
        [StringLength(255)]
        public string Bio { get; set; } = "";
        [Required]
        public List<UserRoles> Roles { get; set; } = new List<UserRoles> { UserRoles.GUEST };
        public Contact? Contact { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                Id,
                Avatar,
                UID,
                Name,
                Email,
                Password,
                Bio,
                Roles,
                Contact,
                Blogs = Blogs.Select(b => new { b.Id, b.Title }), // Example projection
                Comments = Comments.Select(c => new { c.Id, c.Content }) // Example projection
            }, new JsonSerializerOptions
            {
                WriteIndented = true // Optional: formats output for readability
            });
        }
    }
}
