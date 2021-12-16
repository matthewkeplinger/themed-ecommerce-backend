using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories;
            return Ok(categories);
        }

        //GET BY ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            _context.Categories.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryID == id);
            category.Name = value.Name;
            _context.SaveChanges();
            return Ok(category);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryID == id);
            _context.Remove(category);
            _context.SaveChanges();
            return Ok();
        }
    }
}
