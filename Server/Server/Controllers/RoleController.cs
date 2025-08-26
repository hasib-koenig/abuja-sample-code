using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Models;



namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RoleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>>  Get()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return Ok(roles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.RoleId == id);
            return Ok(role);
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string roleName)
        {
            var roleToCreate = new Role
            {
                RoleName = roleName,
            };

            await _dbContext.Roles.AddAsync(roleToCreate);
            _dbContext.SaveChanges();

            return CreatedAtAction("Get",new {Id = roleToCreate.RoleId},roleToCreate);
        }

    }
}
