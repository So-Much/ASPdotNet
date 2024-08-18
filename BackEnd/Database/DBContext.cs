using Microsoft.EntityFrameworkCore;

namespace BackEnd.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        
        //Add tables here
        public DbSet<Tables.User> Users { get; set; }
        public DbSet<Tables.Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tables.User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<Tables.Contact>(c => c.UserId)
                .IsRequired(false);
        }
    }
}
