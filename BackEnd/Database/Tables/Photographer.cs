namespace BackEnd.Database.Tables
{
    public class Photographer : User
    {
        public string Camera { get; set; } = "";
        public string Lens { get; set; } = "";
        public string Specialization { get; set; } = "";
        public string Portfolio { get; set; } = "";
        public Photographer()
        {
            if (!Roles.Contains(UserRoles.PHOTOGRAPHER))
            {
                Roles.Add(UserRoles.PHOTOGRAPHER);
            }
        }
    }
}
