using BackEnd.Database.Tables;
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
        public async Task<ActionResult<Blog>> CreateBlog(Blog blog)
        {

            return Ok("Create a Blog");
        }
    }
}
