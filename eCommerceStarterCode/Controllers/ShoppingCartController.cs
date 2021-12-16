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
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //GET ALL FOR TESTING ONLY PLEASE GOD DELETE
        [HttpGet]
        public IActionResult Get()
        {
            var allCarts = _context.ShoppingCarts;
            return Ok(allCarts);
        }

        //GET
        //[HttpGet, Authorize]
        //public IActionResult Get()
        //{
        //    var shoppingCart = _context.ShoppingCarts.Include(eas => eas.Product.User) ;
        //    var userShoppingCart = shoppingCart.Where(eas => eas.UserId == User.FindFirstValue("id"));
        //    return Ok(userShoppingCart);
        //}

        // PUT api/shoppingcart
        [HttpPut, Authorize]
        public IActionResult Post( [FromBody] ShoppingCart value)
        {
            var userShoppingCart = _context.ShoppingCarts.Where(eas => eas.UserId == User.FindFirstValue("id"));

            if(userShoppingCart.Where(eas => eas.ProductID == value.ProductID).Count() > 0)
            {
                var itemInCart = userShoppingCart.FirstOrDefault(eas => eas.ProductID == value.ProductID);
                itemInCart.UserId = User.FindFirstValue("id");
                itemInCart.Quantity += 1;
                _context.SaveChanges();
                return Ok(itemInCart);
            }
            else
            {
                value.Quantity = 1;
                value.UserId = User.FindFirstValue("id");
                _context.ShoppingCarts.Add(value);
                _context.SaveChanges();
                return StatusCode(201, value);
            }
        }

        //PATCH (to update qty) api/ShoppingCartController/{id}
        [HttpPatch("{id}"), Authorize]
        public IActionResult Patch(int id, [FromBody] ShoppingCart value)
        {
            var itemInCart = _context.ShoppingCarts.Where(eas => eas.ShoppingCartID == id).FirstOrDefault(eas => eas.UserId == User.FindFirstValue("id"));
            itemInCart.Quantity = value.Quantity;
           
            if(itemInCart.Quantity <= 0)
            {
                _context.Remove(itemInCart);
                _context.SaveChanges();
            }
            _context.SaveChanges();
            return Ok(itemInCart);
        }

        // DELETE api/ShoppingCartController/{id}
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            var itemInCart = _context.ShoppingCarts.Where(eas => eas.ShoppingCartID == id).FirstOrDefault(eas => eas.UserId == User.FindFirstValue("id"));
            _context.Remove(itemInCart);
            _context.SaveChanges();
            return Ok();
        }
    }
}
