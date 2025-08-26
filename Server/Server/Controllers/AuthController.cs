using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.DTOs.User;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AuthController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login()
        {

            BCrypt.Net.BCrypt.Verify("salted", "sale");
            return Ok("Something");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRequestDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Empty User can not be createdd");
            }


            var existingUser = await _dbContext.Users.FirstOrDefaultAsync((user) => user.UserEmail.ToLower() == dto.UserEmail.ToLower());

            if (existingUser is not null)
            {
                return BadRequest("User Already exist with this email id");
            }

            var existingRole = await _dbContext.Roles.FirstOrDefaultAsync((role) => role.RoleName == dto.RoleName);

            if (existingRole == null)
            {
                var roleToCreate = new Role { RoleName = dto.RoleName.ToLower() };
                await _dbContext.Roles.AddAsync(roleToCreate);
                _dbContext.SaveChanges();
                existingRole = roleToCreate;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.UserPassword);

            var user = new User
            {
                UserEmail = dto.UserEmail,
                UserName = dto.UserName,
                UserPassword = hashedPassword,
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = existingRole
                    }
                }
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok("User created Succesfully");
        }
    }
}


