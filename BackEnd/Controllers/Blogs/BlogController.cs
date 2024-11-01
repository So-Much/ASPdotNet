using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

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
        public IActionResult GetBlogs()
        {
            return Ok("Get all Blogs");
        }
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Blog>> CreateBlog([FromForm] IFormCollection form)
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
                return Ok(blog);
            } catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Create a Blog");
                return BadRequest("Error Create a Blog");
             }
        }
    }
}
