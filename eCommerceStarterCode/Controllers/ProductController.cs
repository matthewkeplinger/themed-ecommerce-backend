using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.Include("Category").Include("User");
            return Ok(products);
        }

        //GET BY ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var products = _context.Products.FirstOrDefault(product => product.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        //POST NEW PRODUCT
        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product value)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductID == id);
            product.Name = value.Name;
            product.Price = value.Price;
            product.Description = value.Description;
            product.AverageRating = value.AverageRating;
            product.CategoryID = value.CategoryID;
            _context.SaveChanges();
            return Ok(product);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductID == id);
            _context.Remove(product);
            _context.SaveChanges();
            return Ok();
        }




    }
}
