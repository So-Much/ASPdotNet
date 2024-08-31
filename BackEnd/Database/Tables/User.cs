using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Database.Tables
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UID { get; set; } = "";
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = "Default User";
        [StringLength(255)]
        public string Email { get; set; } = "";
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = "";
        [StringLength(255)]
        public string Bio { get; set; } = "";
        [Required]
        public List<UserRoles> Roles { get; set; } = new List<UserRoles>([UserRoles.GUEST]);
        public Contact? Contact { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
