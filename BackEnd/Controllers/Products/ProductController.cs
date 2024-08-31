using BackEnd.Database;
using BackEnd.Database.Tables;
using Microsoft.AspNetCore.Mvc;
using BackEnd.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DBContext _DbContext;
        private readonly ILogger<ProductController> _Logger;
        public ProductController(DBContext DbContext, ILogger<ProductController> logger)
        {
            _DbContext = DbContext;
            _Logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _DbContext.Products
                    //.Include(p => p.Category)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Images = p.Images,
                        Price = p.Price,
                        Stock = p.Stock,
                        Brand = p.Brand

                    })
                    .ToListAsync();

                return Ok(products);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when get all Products");
                return BadRequest("An Error when get all Products");
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            try
            {
                var product = await _DbContext.Products
                    //.Include(p => p.Category)
                    .Where(p => p.Id == Id)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Images = p.Images,
                        Price = p.Price,
                        Stock = p.Stock,
                        Brand = p.Brand
                    })
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when get Product");
                return BadRequest("An Error when get Product");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                //{
                //    Name : ''
                //    Description : ''
                //    Images : ''
                //    Price : ''
                //    Stock : ''
                //    Brand : ''
                //}
                //check category is exist add that product with that category,
                //    if not exist create new category and add that product with that category
                await _DbContext.Products.AddAsync(product);
                await _DbContext.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "An Error when create Product");
                return BadRequest("An Error when create Product");
            }
        }
    }
}
