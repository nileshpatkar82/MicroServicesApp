using Azure.Core;
using CatalogService.Database;
using CatalogService.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public AppDbContext _db;
        public CatalogController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var model = _db.Products.Find(id);
            return model;
        }

        [HttpPost]
        public IActionResult Add(Product model)
        {
            try
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return CreatedAtAction("Add", model);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
