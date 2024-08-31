using Microsoft.EntityFrameworkCore;

namespace BackEnd.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        
        //Add tables here
        public DbSet<Tables.User> Users { get; set; }
        public DbSet<Tables.Contact> Contacts { get; set; }
        public DbSet<Tables.Product> Products { get; set; }
        public DbSet<Tables.Blog> Blogs { get; set; }
        public DbSet<Tables.Post> Posts { get; set; }
        public DbSet<Tables.Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
