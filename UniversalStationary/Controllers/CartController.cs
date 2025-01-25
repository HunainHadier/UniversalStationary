using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversalStationary.Models;

namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public CartController(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromForm] CartViewModel cartItem)
        {
            if (cartItem == null || cartItem.ProductId == Guid.Empty || cartItem.Quantity <= 0)
            {
                return BadRequest("Invalid cart item.");
            }

            // Fetch existing item
            var existingCartItem = await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };

                await _dbContext.CartItems.AddAsync(newCartItem);
            }

            await _dbContext.SaveChangesAsync();
            return Ok(new { Message = "Item added to cart successfully." });
        }




        [HttpDelete("RemoveFromCart/{productId}")]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            // Find the cart item by ProductId
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound("Item not found in cart."); // Return 404 if not found
            }

            // Remove the item from the database
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Item removed from cart successfully." }); // Return success message
        }


        // Get All Cart Items
        [HttpGet("GetCartItems")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _dbContext.CartItems.ToListAsync();
            return Ok(cartItems);
        }


        [HttpGet("GetCartcatigory")]
        public IActionResult GetProducts(string? productname, string? category)
        {
            var query = _dbContext.addproduct.AsQueryable();

            if (!string.IsNullOrEmpty(productname))
            {
                query = query.Where(p => p.productname.Contains(productname));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            var products = query.ToList();
            return Ok(new { products });
        }



    }
}
