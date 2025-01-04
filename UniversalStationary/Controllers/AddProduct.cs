using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversalStationary.Models;

namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProduct : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public AddProduct(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost("Addproduct")]
        public async Task<IActionResult> cretaeproducts([FromForm] AddProductView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { massgae = "Invalid Data Provided" });

            }
            string ProductPicturePath = null;

            // Handle image upload if provided
            if (model.productpicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.productpicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.productpicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    ProductPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

                var product = new AddProductModel
                {
                    productname = model.productname,
                    productpicture = ProductPicturePath,
                    Brand = model.Brand,
                    Category = model.Category,
                    description = model.description,
                    Price = model.Price,
                    Discount = model.Discount,
                    Stock = model.Stock,
                    Trendingproducts = model.Trendingproducts,
                    FeaturedProduct = model.FeaturedProduct,
                    NewArrival = model.NewArrival,


                };

                await _dbContext.addproduct.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Product Add successfully", product });

            }

        // Read all product data

        [HttpGet("Getalldata")]
        public async Task<IActionResult> getalldata()
        {
            var product = await _dbContext.addproduct.ToListAsync();
            return Ok(product);
        }




      
        

        // delete data 
        [HttpDelete("deleteproduvt/ {id}")]

        public async Task<IActionResult> deletedata(Guid id)
        {

            var products = await _dbContext.addproduct.FindAsync(id);
            if(products == null)
            {

                return BadRequest(new { massage = "Invalid data parovided" });

            }

            _dbContext.addproduct.Remove(products);
            await _dbContext.SaveChangesAsync();
            return Ok(new { massage = "Product delete Successfully" });
        }


        [HttpGet("countallproduct")]

        public IActionResult countdata()
        {
            var alldata = _dbContext.addproduct.Count();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }


        [HttpGet("CountNewArrival")]

        public IActionResult CountNewArrival()
        {
            var alldata = _dbContext.addproduct.Where(a => a.NewArrival == true).Count();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }
        [HttpGet("GetNewArrival")]

        public async Task<IActionResult> GetNewArrival()
        {
            var alldata = await _dbContext.addproduct.Where(a => a.NewArrival == true).ToListAsync();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }




        [HttpGet("Getfetuerdproduct")]

        public async Task<IActionResult> Getfetuerdproduct()
        {
            var alldata = await _dbContext.addproduct.Where(a => a.FeaturedProduct == true).ToListAsync();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }








        [HttpGet("GetTrendingproducts")]
        public async Task<IActionResult> GetTrendingproducts()
        {
            var alldata = await _dbContext.addproduct.Where(a => a.Trendingproducts == true).ToListAsync();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }




        [HttpGet("CountTrendingproducts")]

        public IActionResult CountTrendingproducts()
        {
            var alldata = _dbContext.addproduct.Where(a => a.Trendingproducts == true).Count();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }

        [HttpGet("CountFeatured")]

        public IActionResult CountFeatured()
        {
            var alldata = _dbContext.addproduct.Where(a => a.FeaturedProduct == true).Count();
            if (alldata == null)
            {
                return BadRequest(new { massage = "no product here" });

            }
            return Ok(alldata);


        }



        // Update
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProdct(Guid id, [FromForm] AddProductView model)
        {

            string ProductPicturePath = null;

            // Handle image upload if provided
            if (model.productpicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.productpicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.productpicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    ProductPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }




            var existingproduct = await _dbContext.addproduct.FindAsync(id);
            if (existingproduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            existingproduct.productname = model.productname;
            existingproduct.description = model.description;
            existingproduct.Category = model.Category;
            existingproduct.Brand = model.Brand;
            existingproduct.productpicture = ProductPicturePath;
            existingproduct.Discount = model.Discount;
            existingproduct.Stock = model.Stock;
            existingproduct.Price = model.Price;
            existingproduct.NewArrival = model.NewArrival;
            existingproduct.FeaturedProduct = model.FeaturedProduct;
           
            _dbContext.addproduct.Update(existingproduct);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully", existingproduct });
        }












        [HttpGet("SearchProduct")]
        public async Task<IActionResult> SearchProducts(
    [FromQuery] string productName = null)
  
        {
         
            var query = _dbContext.addproduct.AsQueryable();

            // Filters apply karein
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.productname.Contains(productName));
            }


            // Result fetch karein
            var products = await query.ToListAsync();

            // Check if products exist
            if (products == null || !products.Any())
            {
                return NotFound(new { message = "No products found" });
            }

            return Ok(new { message = "Products retrieved successfully", products });
        }







    }

}

        

