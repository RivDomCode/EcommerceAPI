using API.Data;
using API.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]  //Esto le da a nuestra clase poderes de API
    [Route("api/[controller]")]
    public class ProductsController  : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context )
        {
         _context = context;    //Asgnamos el valor del parametro context a la variable _context
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            return  await  _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
            public  async Task<ActionResult<Product>> GetOneProduct(int id){
              return await  _context.Products.FindAsync(id);
        }
    }
}