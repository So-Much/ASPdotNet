using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.DTO;
using BackEnd.Servicers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackEnd.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly DBContext _DbContext;
        private readonly ILogger<Login> _Logger;
        public Login(DBContext DbContext, ILogger<Login> logger)
        {
            _DbContext = DbContext;
            _Logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            //  user:{
            //    email: ''
            //    password: ''
            //}
            try
            {
                var userDB = await _DbContext.Users
                    .Include(u => u.Contact)
                    .Where(u => u.Email.Equals(user.Email))
                    .Select(u => new UserDTO
                    {
                        UID = u.UID,
                        Name = u.Name,
                        Email = u.Email,
                        Password = u.Password,
                        Bio = u.Bio,
                        Roles = u.Roles.Select(r => r.ToString()).ToArray(),
                        Contact = u.Contact
                    })
                    .FirstOrDefaultAsync();
                if (userDB == null)
                {
                    return NotFound("User not found");
                }
                if (!BcryptService.Verify(user.Password, userDB.Password))
                {
                    return BadRequest("Password is incorrect");
                }
                var token = JWTServices.GenerateJwtToken(userDB);

                return Ok(new { token });
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when login User");
                return BadRequest("An Error when login User");
            }
        }
    }
}
