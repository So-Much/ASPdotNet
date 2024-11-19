using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace BackEnd.Controllers.Posts
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DBContext _DbContext;
        private readonly ILogger<PostController> _Logger;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public PostController(DBContext dbContext, ILogger<PostController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _DbContext = dbContext;
            _Logger = logger;
            _WebHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            // Get all posts
            try
            {
                var posts = await _DbContext.Posts.ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get all Posts");
                return BadRequest("Error Get all Posts");
            }
        }
        public class Params
        {
            public string content { get; set; }
        }
        [Authorize]
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<string>> CreatePost([FromForm] IFormCollection form)
        {
            //Title
            //Content
            //Images
            //Videos
            //NumLikes
            //NumDislike
            //IsPublished
            //ShareLink
            //Hashtags
            //CreateAt
            //FK_BlogId
            //Blog
            //Comments
            try
            {
                string hashtagPattern = @"(?<=@)[a-z0-9]+(?=\s)";
                //_Logger.LogInformation("---------------------------------");
                string content = form["content"];
                string title = form["title"];
                bool isPublished = bool.Parse(form["isPublished"]);
                string blogId = form["blogId"];
                List<string> tags = new List<string>();
                foreach (Match match in Regex.Matches(content, hashtagPattern))
                {
                    tags.Add(match.Value);
                }


                //_Logger.LogInformation(content);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // FInd user by userId and create post
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Get images (Base64) and videos(URL) from content to handle
                // Process images and videos from content
                var (updatedContent, imageUrls, videoUrls) = await ImageProcessing.ProcessImagesAndVideos(content, user);
                // Create post
                var post = new Post
                {
                    Title = title, // get from form body
                    Content = updatedContent,
                    Images = imageUrls,
                    Videos = videoUrls,
                    NumLikes = 0,
                    NumDislike = 0,
                    IsPublished = isPublished, // get from form body
                    Hashtags = tags, //get from form body
                    CreateAt = DateTime.Now,
                    FK_BlogId = 0, // get from form body to know which blog this post belong to
                    Comments = new List<Comment>() // create empty list of comments
                };
                //Find Blog from DB and add that Post to it
                var blog = await _DbContext.Blogs
                    .FirstOrDefaultAsync(b => b.Id == int.Parse(blogId));
                if (blog == null)
                {
                    return NotFound("Blog not found");
                }
                post.Blog = blog;
                _DbContext.Posts.Add(post);
                await _DbContext.SaveChangesAsync();
                return Ok(updatedContent);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Create Post");
                return BadRequest("Error Create Post");
            }
        }

        [HttpPost("img")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<string>> UploadPostImage([FromForm] IFormCollection form)
        {
            try
            {
                //find user is existed?
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                //get image from form data
                var image = form.Files["image"];
                if (image == null)
                {
                    return BadRequest("Image is required");
                }
                //store that img to uploads(static) folder
                //_Logger.LogInformation($"UploadPath: {StaticPathCollection.GetUploadProjectPath()}");
                //_Logger.LogInformation($"StaticPath: {StaticPathCollection.GetStaticPath()}");
                var filename = image.FileName.Split('.')[0];
                var fileExtension = image.FileName.Split('.')[1];
                var filepath = $"post/{user.UID}_{filename}_{Guid.NewGuid().ToString()}.{fileExtension}";
                //_Logger.LogInformation($"Folder Path: {filepath.Substring(0, filepath.LastIndexOf('/'))}");
                var imageUrl = await ImageProcessing.StoreImage(image, filepath);

                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Upload Post Image");
                return BadRequest("Error Upload Post Image");
            }
        }
        [Authorize]
        [HttpGet("allpostsbyuser")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsByUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var posts = await _DbContext.Blogs
                    .Where(b => b.Author.UID == userId)
                    .SelectMany(b => b.Posts)
                    .ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get Posts By User");
                return BadRequest("Error Get Posts By User");
            }
        }
        [HttpGet("getpublishedposts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPublishedPosts()
        {
            try
            {
                var posts = await _DbContext.Posts
                    .Where(p => p.IsPublished)
                    .ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get Published Posts");
                return BadRequest("Error Get Published Posts");
            }
        }
    }
}
