using BackEnd.Database;
using Microsoft.EntityFrameworkCore;

public static class PostgreSQL
{
    public static void ConfigureDatabase(this IServiceCollection services, string ConnectionString)
    {
        // Configure PostgreSQL
        services.AddDbContext<DBContext>(options =>
            options.UseNpgsql(ConnectionString)
        );
    }
}
