using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.DTO;
using BackEnd.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;
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
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            try
            {
                var blogs = await _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .ToListAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get All Blogs");
                return BadRequest("Error Get All Blogs");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogById(int id)
        {
            try
            {
                var blog = await _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .FirstOrDefaultAsync(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [HttpGet("allblogsbyuser")]
        [Authorize]
        public async Task<ActionResult<Blog>> GetAllBlogsByUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return NotFound("No User Found");
                }
                var blogs = await _DbContext.Blogs
                    .Include(b => b.Author)
                    .Include(b => b.Posts)
                    .Where(b => b.Author!.UID.Equals(user!.UID))
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
        public async Task<ActionResult<IEnumerable<Blog>>> GetPublishedBlogs([FromQuery] int pageNumber = 1, [FromQuery] int limit = 10)
        {
            try
            {
                if (pageNumber <= 0 || limit <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                // Calculate total count for pagination metadata
                var totalBlogs = await _DbContext.Blogs
                    .Where(b => b.IsPublished)
                    .CountAsync();

                // Fetch the paginated data
                var blogs = await _DbContext.Blogs
            .Include(b => b.Posts) // Include Posts to access their data
            .Where(b => b.IsPublished)
            .Skip((pageNumber - 1) * limit)
            .Take(limit)
            .Select(b => new
            {
                BlogId = b.Id,
                Title = b.Title,
                Description = b.Description,
                CreateAt = b.CreateAt,
                Author = b.Author,
                Image = b.Image,
                Posts = b.Posts.Select(p => new
                {
                    PostId = p.Id,
                    Title = p.Title,
                    Hashtags = p.Hashtags
                }),
                TopHashtags = b.Posts
                    .SelectMany(p => p.Hashtags) // Flatten all hashtags from posts
                    .GroupBy(h => h) // Group by the hashtag itself
                    .Select(g => new
                    {
                        Hashtag = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(h => h.Count) // Sort by count
                    .Take(3) // Top 3 hashtags
            })
            .ToListAsync();
                var paginationMetadata = new
                {
                    TotalCount = totalBlogs,
                    PageSize = limit,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalBlogs / (double)limit)
                };

                // Add metadata to response headers
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

                return Ok(blogs);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get Published Blogs");
                return BadRequest("Error Get Published Blogs");
            }
        }
        [HttpPut("img/{id}")]
        [Authorize]
        public async Task<ActionResult<String>> updateBlogImg(int id, [FromForm] IFormCollection form)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);

                var blog = await _DbContext.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return NotFound("Blog does not exist!");
                }

                var image = form.Files["image"] ?? null;
                var filename = image.FileName.Split('.')[0];
                var fileExtension = image.FileName.Split('.')[1];
                var filepath = $"blog/{user.Name}_{filename}_{Guid.NewGuid().ToString()}.{fileExtension}";
                string blogimg = await ImageProcessing.StoreImage(image, filepath);

                blog.Image = blogimg;
                await _DbContext.SaveChangesAsync();
                return Ok(blogimg);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Blog>> updateBlog(int id, [FromBody] Dictionary<string, object> updates)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return NotFound("No User Found");
                }
                var blog = await _DbContext.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return BadRequest("Blog is deleted or something went wrong");
                }
                // Cập nhật chỉ các trường không null
                foreach (var update in updates)
                {
                    switch (update.Key.ToLower())
                    {
                        case "title":
                            blog.Title = update.Value?.ToString();
                            break;
                        case "description":
                            blog.Description = update.Value?.ToString();
                            break;
                        case "ispublished":
                            if (bool.TryParse(update.Value?.ToString(), out var isPublished))
                            {
                                blog.IsPublished = isPublished;
                            }
                            break;
                        default:
                            // Nếu key không hợp lệ, bỏ qua
                            break;
                    }
                }

                // Lưu thay đổi vào database
                _DbContext.Blogs.Update(blog);
                await _DbContext.SaveChangesAsync();

                return Ok(blog);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error updating blog with ID {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Blog>> deleteBlog(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return BadRequest("User is not found!");
                }
                var blog = await _DbContext.Blogs
                    .FirstOrDefaultAsync(b => b.Id == id && b.FK_UserId == user.Id);
                if (blog == null)
                {
                    return BadRequest("User is Unthorized or Blog doesn't exist!");
                }
                //when delete a blog -> delete all posts in this blog
                foreach (var post in blog.Posts)
                {
                    _DbContext.Posts.Remove(post);
                }
                _DbContext.Blogs.Remove(blog);
                await _DbContext.SaveChangesAsync();
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}/getpublishposts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPublishedPosts(int id,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int limit = 10
        )
        {
            try
            {
                if (pageNumber <= 0 || limit <= 0)
                {
                    return BadRequest("Page number and limit must be greater than zero.");
                }

                var blog = await _DbContext.Blogs
                    .Include(b => b.Posts)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (blog == null)
                {
                    return NotFound("Blog not found");
                }

                // Lấy tổng số bài viết để thêm metadata phân trang
                var totalPosts = await _DbContext.Posts
                    .Where(p => p.FK_BlogId == blog.Id && p.IsPublished)
                    .CountAsync();

                var posts = await _DbContext.Posts
                    .Where(p => p.FK_BlogId == blog.Id && p.IsPublished)
                    .OrderByDescending(p => p.CreateAt)
                    .Skip((pageNumber - 1) * limit)
                    .Take(limit)
                    .ToListAsync();
                // Metadata phân trang
                var paginationMetadata = new
                {
                    TotalCount = totalPosts,
                    PageSize = limit,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(totalPosts / (double)limit)
                };

                // Trả metadata trong header
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [HttpGet("{id}/getpopularposts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPopularPosts(int id)
        {
            try
            {
                var blog = await _DbContext.Blogs
                    .Include(b => b.Posts)
                    .FirstOrDefaultAsync(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                // Lấy danh sách 3 bài đăng có tương tác cao nhất
                var popularPosts = await _DbContext.Posts
                    .Where(p => p.FK_BlogId == id && p.IsPublished)
                    .OrderByDescending(p => p.NumLikes + p.NumDislike)
                    .Take(3)
                    .ToListAsync();

                return Ok(popularPosts);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [HttpGet("{id}/gettophasgtag")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopHashtags(int id)
        {
            try
            {
                var blog = await _DbContext.Blogs
                    .Include(b => b.Posts)
                    .FirstOrDefaultAsync(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                // Lấy danh sách 3 hashtag phổ biến nhất
                var topHashtags = blog.Posts
                    .SelectMany(p => p.Hashtags) // Flatten all hashtags from posts
                    .GroupBy(h => h) // Group by the hashtag itself
                    .Select(g => new
                    {
                        Hashtag = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(h => h.Count) // Sort by count
                    .Take(5); // Top 3 hashtags

                return Ok(topHashtags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [HttpGet("{id}/getpostbyhashtag")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostByHashtag(
            int id, [FromQuery] string hashtag)
        {
            try
            {
                var blog = await _DbContext.Blogs
                    .Include(b => b.Posts)
                    .FirstOrDefaultAsync(b => b.Id == id);
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                // Lấy danh sách bài viết chứa hashtag
                var posts = blog.Posts
                    .Where(p => p.Hashtags.Contains(hashtag))
                    .ToList();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
    }
}
