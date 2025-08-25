using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Context;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CategoryController(AppDbContext dbContext) { 
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Create()
        {
           
      
            return Ok(_dbContext.Categories.ToList());
        }


        [HttpPost]
        public ActionResult Create([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return Ok("Category Created");
        }
    }
}
