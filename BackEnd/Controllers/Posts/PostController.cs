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
                string hashtagPattern = @"#[a-z0-9_]+";
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
                return Ok(post);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Create Post");
                return BadRequest("Error Create Post:\n" + ex.ToString());
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
                var filepath = $"post/{user.Name}_{filename}_{Guid.NewGuid().ToString()}.{fileExtension}";
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
        //[Authorize]
        //[HttpGet("allpostsbyuser")]
        //public async Task<ActionResult<IEnumerable<Post>>> GetAllPostsByUser()
        //{
        //    try
        //    {
        //        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var user = await _DbContext.Users
        //            .FirstOrDefaultAsync(u => u.UID == userId);
        //        var blogs = _DbContext.Blogs.
        //    } catch(Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error!");
        //    }
        //}
        [HttpGet("getpostsfromblog")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsFromBlog(int blogId)
        {
            try
            {
                var posts = await _DbContext.Posts
                    .Where(p => p.FK_BlogId == blogId)
                    .ToListAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error Get Posts From Blog");
                return BadRequest("Error Get Posts From Blog");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            try
            {
                var post = await _DbContext.Posts
                    .Include(p => p.Blog)
                    //.Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if(post == null)
                {
                    return NotFound("Post is not found");
                }

                ////begin


                //string content = post.Content;
                //var imageUrls = new List<string>();
                //var videoUrls = new List<string>();
                //// Pattern to match base64 images
                //string pattern = @"data:image\/([a-zA-Z]*);base64,([^\""]*)";

                ////Find all matches in content
                //MatchCollection matches = Regex.Matches(content, pattern);
                ////with any match -> restore this picture and store it to uploads folder
                //foreach (Match match in matches)
                //{
                //    var imageType = match.Groups[1].Value;
                //    var base64Data = match.Groups[2].Value;
                //    // Store image with base64 format
                //    var filepath = $"test/post/_{Guid.NewGuid().ToString()}.{imageType}";
                //    var imagePath = await ImageProcessing.StoreImageWithBase64Format(base64Data, filepath);
                //    //replace base64 image with image static path
                //    content = content.Replace(match.Value, imagePath);
                //    imageUrls.Add(imagePath);
                //}

                ////end

                return Ok(post);
            } catch(Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<Post>> UpdatePost(int id, [FromForm] IFormCollection form)
        {
            try
            {
                string hashtagPattern = @"#[a-z0-9_]+";
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return Unauthorized("User not found");
                }
                var post = await _DbContext.Posts
                    .Where(p => p.Blog.Author.Id == user.Id)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (post == null)
                {
                    return Unauthorized("Post not found");
                }

                var content = form["content"];
                var isPublished = bool.Parse(form["isPublished"]);

                List<string> tags = new List<string>();
                foreach (Match match in Regex.Matches(content, hashtagPattern))
                {
                    tags.Add(match.Value);
                }
                //handle Content before update
                var (updatedContent, imageUrls, videoUrls) = await ImageProcessing.ProcessImagesAndVideos(content, user);

                post.Content = updatedContent;
                post.IsPublished = isPublished;
                post.Hashtags = tags;
                post.Images = imageUrls;
                post.Videos = videoUrls;

                await _DbContext.SaveChangesAsync();

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                if (user == null)
                {
                    return Unauthorized("User not found");
                }
                var post = await _DbContext.Posts
                    .Where(p => p.Blog.Author.Id == user.Id)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (post == null)
                {
                    return Unauthorized("Post not found");
                }
                _DbContext.Posts.Remove(post);
                await _DbContext.SaveChangesAsync();
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error!");
            }
        }
    }
}
