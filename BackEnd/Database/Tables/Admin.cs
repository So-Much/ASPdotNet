namespace BackEnd.Database.Tables
{
    public class Admin : User
    {
        public Admin()
        {
            if (!Roles.Contains(UserRoles.ADMIN))
            {
                Roles.Add(UserRoles.ADMIN);
            }
        }
    }
}
