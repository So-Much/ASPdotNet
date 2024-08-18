namespace BackEnd.Database.Tables
{
    public class Customer : User
    {
        public int history_cost { get; set; } = 0;
        public Customer()
        {
            if (!Roles.Contains(UserRoles.CUSTOMER))
            {
                Roles.Add(UserRoles.CUSTOMER);
            }
        }
    }
}
