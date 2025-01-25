using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversalStationary.Models;

namespace UniversalStationary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyNowController : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public BuyNowController(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }




    }

}
