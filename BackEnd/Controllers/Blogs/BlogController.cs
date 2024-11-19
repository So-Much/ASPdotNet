using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.DTO;
using BackEnd.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackEnd.Controllers.Blogs
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        //Title
        //Description
        //Image
        //CreateAt
        //IsPublished
        //FK_UserId
        //Author
        //Posts
        private readonly ILogger<BlogController> _Logger;
        private readonly DBContext _DbContext;
        public BlogController(ILogger<BlogController> logger, DBContext dbContext)
        {
            _Logger = logger;
            _DbContext = dbContext;
        }
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            try
            {
                var blogs = _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .Select(b => new BlogDTO(b))
                    .ToListAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get All Blogs");
                return BadRequest("Error Get All Blogs");
            }
        }
        [HttpGet("allblogsbyuser")]
        [Authorize]
        public async Task<ActionResult<BlogDTO>> GetAllBlogsByUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                var blogs = _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .Where(b => b.Author == user)
                    .Select(b => new BlogDTO(b))
                    .ToListAsync();
                if (blogs == null)
                {
                    return NotFound("No Blog Found");
                }
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get All Blogs By User");
                return BadRequest("Error Get All Blogs By User");
            }
        }
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<BlogDTO>> CreateBlog([FromForm] IFormCollection form)
        {
            //Check who is create this blog by user
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);

                // 1. Extract the title and description from the form fields, handling potential nulls
                string title = form["title"].FirstOrDefault() ?? string.Empty;
                string description = form["description"].FirstOrDefault() ?? string.Empty;

                // 2. Retrieve the image file from the form files, safely handling nulls
                var image = form.Files["image"] ?? null;

                // 3. Parse the published status to a boolean, defaulting to false if the value is null or invalid
                bool isPublished = bool.TryParse(form["isPublished"].FirstOrDefault(), out bool published) && published;


                var filename = image.FileName.Split('.')[0];
                var fileExtension = image.FileName.Split('.')[1];
                var filepath = $"blog/{user.Name}_{filename}_{Guid.NewGuid().ToString()}.{fileExtension}";
                string blogimg = await ImageProcessing.StoreImage(image, filepath);

                Blog blog = new Blog
                {
                    Title = title,
                    Description = description,
                    Image = blogimg,
                    CreateAt = DateTime.Now,
                    IsPublished = isPublished,
                    Author = user
                };
                _DbContext.Blogs.Add(blog);
                await _DbContext.SaveChangesAsync();
                BlogDTO blogDTO = new BlogDTO(blog);
                return Ok(blogDTO);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Create a Blog");
                return BadRequest("Error Create a Blog");
            }
        }
        [HttpGet("getpublishedblogs")]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetPublishedBlogs()
        {
            try
            {
                var blogs = _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .Where(b => b.IsPublished)
                    .Select(b => new BlogDTO(b))
                    .ToListAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get Published Blogs");
                return BadRequest("Error Get Published Blogs");
            }
        }
    }
}
