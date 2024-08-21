using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.Servicers;
using BackEnd.utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly DBContext _DbContext;
        private readonly ILogger<Register> _Logger;
        public Register(DBContext DbContext, ILogger<Register> logger)
        {
            _DbContext = DbContext;
            _Logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            //  user:{
            //    name: ''
            //    email: ''
            //    password: ''
            //}
            try
            {
                user.UID = GenerateSystem.GenerateUserId();
                user.Password = BcryptService.HashPassword(user.Password);
                user.Roles = new List<UserRoles> { UserRoles.GUEST, UserRoles.CUSTOMER };
                //  user:{
                //    UserId: 'user{hasdedValue}'
                //    name: user.name
                //    email: user.email
                //    password: {PasswordHashed}
                //}
                _DbContext.Users.Add(user);
                await _DbContext.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when register User");
                return BadRequest("An Error when register User");
            }
        }
    }
}
