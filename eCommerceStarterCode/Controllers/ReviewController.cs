using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }


        //GET ALL REVIEWS (FOR TESTING)
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews.Include("Product").Include("User");
            return Ok(reviews);
        }

        // <baseurl>/api/ReviewController
        [HttpGet("{ReviewID}")]
        public IActionResult Get(int id)
        {
            var reviews = _context.Reviews.Include(r => r.Product).Include(r => r.User).Where(review => review.ProductID == id);
            return Ok(reviews);

        }

        //POST a new Review /api/ReviewController/
        [HttpPost]
        public IActionResult Post([FromBody] Review value)
        {
            _context.Reviews.Add(value);
            var product = _context.Products.FirstOrDefault(product => product.ProductID == value.ProductID);
            _context.SaveChanges();
            return Ok(value);

        }

        //PUT to modify review /api/ReviewController/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review value)
        {
            var review = _context.Reviews.FirstOrDefault(review => review.ReviewID == id);
            review.Rating = value.Rating;
            review.Description = value.Description; //FOR TESTING
            _context.SaveChanges();
            return Ok(review);
    }

        //DELETE /api/ReviewController/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(review => review.ReviewID == id);
            _context.Remove(review);
            _context.SaveChanges();
            return Ok();
        }
    }
}

