using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Aluastetaan tiettokanta yhteys
        NorthwindOriginalContext db = new NorthwindOriginalContext();
        // or NorthwindOriginalContext db =  new();

        //Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            var customers = db.Customers.ToList();
            return Ok(customers);
        }

    }
}
