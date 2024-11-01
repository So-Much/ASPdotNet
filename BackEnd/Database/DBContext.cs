using Microsoft.EntityFrameworkCore;

namespace BackEnd.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        
        //Add tables here
        public DbSet<Tables.User> Users { get; set; }
        public DbSet<Tables.Admin> Admins { get; set; }
        public DbSet<Tables.Customer> Customers { get; set; }
        public DbSet<Tables.Photographer> Photographers { get; set; }
        public DbSet<Tables.Contact> Contacts { get; set; }
        public DbSet<Tables.Product> Products { get; set; }
        public DbSet<Tables.Blog> Blogs { get; set; }
        public DbSet<Tables.Post> Posts { get; set; }
        public DbSet<Tables.Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //inheritance of User table
            modelBuilder.Entity<Tables.User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Tables.Admin>("Admin")
                .HasValue<Tables.Customer>("Customer")
                .HasValue<Tables.Photographer>("Photographer");

            //user-contact Fluent API
            modelBuilder.Entity<Tables.User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<Tables.Contact>(c => c.FK_UserId)
                .IsRequired(false);
            //user-blog Fluent API
            modelBuilder.Entity<Tables.User>()
                .HasMany(u => u.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.FK_UserId)
                .IsRequired(false);
            //blog-post Fluent API
            modelBuilder.Entity<Tables.Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.FK_BlogId)
                .IsRequired(false);
            //post-comment Fluent API
            modelBuilder.Entity<Tables.Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.FK_PostId)
                .IsRequired();
            //user-comment Fluent API
            modelBuilder.Entity<Tables.User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.FK_UserId)
                .IsRequired();
        }
    }
}
