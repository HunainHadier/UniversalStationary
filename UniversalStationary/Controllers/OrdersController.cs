using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversalStationary.Models;

namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public OrdersController(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost("order")]
        public async Task<IActionResult> PlaceOrder([FromForm] OrderRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.ProductId == Guid.Empty)
            {
                return BadRequest("Invalid order details.");
            }

            // 1. Check if an order already exists for this product and user email
            var existingOrder = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.ProductId == orderRequest.ProductId && o.Email == orderRequest.Email);

            if (existingOrder != null)
            {
                return BadRequest("This product has already been ordered by this user.");
            }

            // 2. Create a new order if no existing order found
            var newOrder = new Order
            {
                ProductId = orderRequest.ProductId,
                UserName = orderRequest.UserName,
                Email = orderRequest.Email,
                Address = orderRequest.Address,
                City = orderRequest.City,
                PaymentMethod = orderRequest.PaymentMethod,
                OrderDate = DateTime.Now
            };

            // 3. Save the new order in the database
            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Order placed successfully." });
        }



        [HttpGet("Getalldata")]
        public async Task<IActionResult> getalldata()
        {
            var product = await _dbContext.Orders.ToListAsync();
            return Ok(product);
        }


        // delete data 
        [HttpDelete("deleteOrder/ {id}")]

        public async Task<IActionResult> deletedata(Guid id)
        {

            var products = await _dbContext.Orders.FindAsync(id);
            if (products == null)
            {

                return BadRequest(new { massage = "Invalid data parovided" });

            }

            _dbContext.Orders.Remove(products);
            await _dbContext.SaveChangesAsync();
            return Ok(new { massage = "Order delete Successfully" });
        }


        [HttpPost("cancel")]
        public async Task<IActionResult> CancelOrder([FromForm] CancelOrderRequest cancelOrderRequest)
        {
            if (cancelOrderRequest.ProductId == Guid.Empty || string.IsNullOrEmpty(cancelOrderRequest.UserEmail))
            {
                return BadRequest("Invalid order ID or email.");
            }

            // 1. Fetch the order from the database based on the orderId and userEmail
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.ProductId == cancelOrderRequest.ProductId && o.Email == cancelOrderRequest.UserEmail);

            if (order == null)
            {
                return NotFound("Order not found or you are not authorized to cancel this order.");
            }

            // 2. If the order exists, cancel it (you can either delete or mark it as canceled)
            _dbContext.Orders.Remove(order);

         
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Order canceled successfully." });
        }



    }
}
