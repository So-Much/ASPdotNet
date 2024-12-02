using BackEnd.Database.Tables;

namespace BackEnd.DTO
{
    public class BlogDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsPublished { get; set; }
        public int? FK_UserId { get; set; }
        public string? AuthorName { get; set; } // Optional: Add relevant fields if needed

        public BlogDTO(Blog blog)
        {
            Title = blog.Title;
            Description = blog.Description;
            Image = blog.Image;
            CreateAt = blog.CreateAt;
            IsPublished = blog.IsPublished;
            FK_UserId = blog.FK_UserId;
            AuthorName = blog.Author?.Name; // Assuming User has a Name field
        }
    }
}
