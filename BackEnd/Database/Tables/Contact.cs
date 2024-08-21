using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Database.Tables
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int? FK_UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public string Location { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string SocialMedias { get; set; } = "";
    }
}
