using BackEnd.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Blogs
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok("Get all Blogs");
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Blog>> CreateBlog(Blog blog)
        {
            //Check who is create this blog by user

            return Ok("Create a Blog");
        }
    }
}
