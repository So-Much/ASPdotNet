using BackEnd.Database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Posts
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return Ok("Get all Posts");
        }
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {

            return Ok("Create a Post");
        }
        [HttpPost("upload")]
        public async Task<ActionResult<String>> UploadPostImage(IFormFile file)
        {
            return Ok("Upload Post Image");
        }
    }
}
