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
            try
            {
                _Logger.LogInformation("---------------------------------");
                string content = form["content"];
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
                var (updatedContent, imageUrls, videoUrls) = await ProcessImagesAndVideos(content, user);
                // Create post
                var post = new Post
                {
                    Content = updatedContent,
                    Images = imageUrls,
                    Videos = videoUrls,
                    NumLikes = 0,
                    NumDislike = 0,
                    IsPublished = true, // get from form body
                    Hashtags = new List<string>(), //get from form body
                    CreateAt = DateTime.Now,
                    FK_BlogId = 0, // get from form body to know which blog this post belong to
                    Comments = new List<Comment>() // create empty list of comments
                };
                _DbContext.Posts.Add(post);
                await _DbContext.SaveChangesAsync();
                return Ok(content);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Create Post");
                return BadRequest("Error Create Post");
            }
        }
        private async Task<(string UpdatedContent, List<string> ImageUrls, List<string> VideoUrls)> ProcessImagesAndVideos(string content, User user)
        {
            var imageUrls = new List<string>();
            var videoUrls = new List<string>();
            // Pattern to match base64 images
            string pattern = @"data:image\/([a-zA-Z]*);base64,([^\""]*)";

            //Find all matches in content
            MatchCollection matches = Regex.Matches(content, pattern);
            _Logger.LogInformation("________In Method logger________");
            //with any match -> restore this picture and store it to uploads folder
            foreach (Match match in matches)
            {
                var imageType = match.Groups[1].Value;
                var base64Data = match.Groups[2].Value;
                _Logger.LogInformation($"User name: {user.Name}");
                _Logger.LogInformation($"Image Type: {imageType}");
                _Logger.LogInformation($"Base64 Data: {base64Data}");
                // Store image with base64 format
                var filepath = $"post/{user.UID}_{Guid.NewGuid().ToString()}.{imageType}";
                var imagePath = await ImageProcessing.StoreImageWithBase64Format(base64Data, filepath);
                _Logger.LogInformation("----------------------------------");
                _Logger.LogInformation($"Image Path: {imagePath}");
                //replace base64 image with image static path
                content.Replace(match.Value, imagePath);
                imageUrls.Add(imagePath);
            }
            return (content, imageUrls, videoUrls);
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
    }
}
