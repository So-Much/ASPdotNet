using BackEnd.Database.Tables;

namespace BackEnd.DTO
{
    public class UserDTO
    {
        public string UID { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public string[] Roles { get; set; }
        public Contact? Contact { get; set; }
    }
}
