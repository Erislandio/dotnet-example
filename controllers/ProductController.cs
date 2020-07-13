using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{

    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(product => product.Category).ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Product>> Get([FromServices] DataContext context, int id)
        {

            var product = await context.Products.Include(product => product.Category).FirstOrDefaultAsync(product => product.Id == id);
            return product;

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {

                context.Products.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }

}