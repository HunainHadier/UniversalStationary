using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversalStationary.Models;

namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoChange : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public LogoChange(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost("Changelogo")]
        public async Task<IActionResult> changelogo([FromForm] LogoChangeView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { massgae = "Invalid Data Provided" });

            }
            string ProductPicturePath = null;

            // Handle image upload if provided
            if (model.logofile != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.logofile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.logofile.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    ProductPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }


            string sitebannersPicturePath = null;
            if (model.sitebanners != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.sitebanners.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.sitebanners.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    sitebannersPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }
            var chagelogo = new LogoChangeModel
            {
               SiteName = model.SiteName,
               sitediscription = model.sitediscription,
               gmail = model.gmail,
               address = model.address,
               Phonenumber = model.Phonenumber,
                logofile = ProductPicturePath,
                sitebanners = sitebannersPicturePath


            };

            await _dbContext.logochanges.AddAsync(chagelogo);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Logo Change successfully", chagelogo });

        }

   

        // Read all product data

        [HttpGet("Getalldata")]
        public async Task<IActionResult> getalldata()
        {
            var logo = await _dbContext.logochanges.ToListAsync();
            return Ok(logo);
        }



        // delete data 
        [HttpDelete("deletelogo/ {id}")]

        public async Task<IActionResult> deletedata(Guid id)
        {

            var logo = await _dbContext.logochanges.FindAsync(id);
            if (logo == null)
            {

                return BadRequest(new { massage = "Invalid data parovided" });

            }

            _dbContext.logochanges.Remove(logo);
            await _dbContext.SaveChangesAsync();
            return Ok(new { massage = "Product delete Successfully" });
        }




        // Update
        [HttpPut("UpdateSetting/{id}")]
        public async Task<IActionResult> UpdateSetting(Guid id, [FromForm] LogoChangeView model)
        {

            string ProductPicturePath = null;

            // Handle image upload if provided
            if (model.logofile != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.logofile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.logofile.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    ProductPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }


            string bannersPicturePath = null;

            // Handle image upload if provided
            if (model.sitebanners != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "AllImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.sitebanners.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.sitebanners.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    bannersPicturePath = Path.Combine("AllImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }


            var existingproduct = await _dbContext.logochanges.FindAsync(id);
            if (existingproduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            existingproduct.logofile = ProductPicturePath;
            existingproduct.SiteName = model.SiteName;
            existingproduct.address = model.address;
            existingproduct.Phonenumber = model.Phonenumber;
            existingproduct.gmail = model.gmail;
            existingproduct.sitediscription = model.sitediscription;
            existingproduct.sitebanners = bannersPicturePath;

            _dbContext.logochanges.Update(existingproduct);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully", existingproduct });
        }








    }
}
