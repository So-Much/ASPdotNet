﻿using BackEnd.Database;
using BackEnd.Database.Tables;
using BackEnd.DTO;
using BackEnd.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _DbContext.Users
                    .Include(u => u.Contact)
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
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when get all Users");
                return BadRequest("An Error when get all Users");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("{UID}")]
        public async Task<ActionResult<User>> GetUser(string UID)
        {
            try
            {
                var userDTO = await _DbContext.Users.Include(u => u.Contact)
                    .Where(u => u.UID.Equals(UID))
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
                if (userDTO == null)
                {
                    return NotFound();
                }
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when find product by id!");
            }
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                //Veryfied user if necessary
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { UID = user.UID }, user);


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
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{UID}")]
        public async Task<ActionResult<User>> PutUser(string UID, User user)
        {
            if (UID != user.UID)
            {
                return BadRequest();
            }
            try
            {
                //change information of that user and save that user to return for update client side
                _DbContext.Entry(user).State = EntityState.Modified;
                await _DbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when update user!");
            }
            return Ok(user);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{UID}")]
        public async Task<ActionResult<User>> DeleteUser(string UID)
        {
            var user = await _DbContext.Users.FindAsync(UID);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                _DbContext.Users.Remove(user);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when delete user!");
            }
            return Ok("User is Deleted!");
        }
        [Authorize]
        [HttpGet("isloggedin")]
        public async Task<ActionResult<bool>> IsLoggedIn()
        {
            return Ok(true);
        }
        [Authorize]
        [HttpPost("uploadavatar")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult<string>> UploadAvatar([FromForm] IFormCollection form)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _DbContext.Users
                    .FirstOrDefaultAsync(u => u.UID == userId);
                //get image from form data
                var image = form.Files["avatar"];
                if (image == null)
                {
                    return BadRequest("Avatar is required");
                }
                var filename = image.FileName.Split('.')[0];
                var fileExtension = image.FileName.Split('.')[1];
                var filepath = $"avatar/{user.Name}_{filename}_{Guid.NewGuid().ToString()}.{fileExtension}";
                var avatar = await ImageProcessing.StoreImage(image, filepath);
                return Ok(avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return StatusCode(500, "Error when upload avatar!");
            }
        }
    }
}
