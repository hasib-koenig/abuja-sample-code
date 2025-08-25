using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.DTOs.Product;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
           var products = await _dbContext.Products.ToListAsync();
           return Ok(products);
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync((product) => product.ProductId == id);

            if(product == null)
            {
                return NotFound($"Product with {id} does not exist");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] CreateRequestDTO dto)
        {

            var category = await _dbContext.Categories.FirstOrDefaultAsync(category => category.Name == dto.CategoryName);


            if (category == null)
            {
                var categoryToCreate = new Category
                {
                    Name = dto.CategoryName
                };

                await _dbContext.Categories.AddAsync(categoryToCreate);
                await _dbContext.SaveChangesAsync();
                category = categoryToCreate;
            }

            var productToCreate = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = category.CategoryId,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Image = dto.Image
            };

            await _dbContext.Products.AddAsync(productToCreate);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new {id = productToCreate.ProductId},productToCreate);
        }
    }
}


//api/product/5