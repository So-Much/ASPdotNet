using BackEnd.Database;
using BackEnd.Database.Tables;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext _DbContext;
        private readonly ILogger<UserController> _Logger;
        public UserController(DBContext DbContext, ILogger<UserController> logger)
        {
            _DbContext = DbContext;
            _Logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _DbContext.Users
                    .Include(u => u.Contact)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when get all Users");
                return BadRequest("An Error when get all Users");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _DbContext.Users.Include(u => u.Contact)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when find product by id!");
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                //Veryfied user if necessary
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser),new { id = user.Id }, user);


                //// Kiểm tra và tạo Contact nếu không có sẵn
                //if (user.Contact != null)
                //{
                //    user.Contact.UserId = user.Id; // Liên kết Contact với User
                //    _DbContext.Contacts.Add(user.Contact);
                //}

                //_DbContext.Users.Add(user);
                //await _DbContext.SaveChangesAsync();

                //return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when add user!");
            }
        }
    }
}
