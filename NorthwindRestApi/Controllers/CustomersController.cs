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
            try
            {
                var customers = db.Customers.ToList();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong! " + e.InnerException);
            }
        }


        //Hakee asiakkaan id n mukaan 
        [HttpGet("{id}")]
        // or [Route("{id}")]
        public ActionResult GetOneCustomersById(string id)
        {
            var customer = db.Customers.Find(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                //return BadRequest("Customer for that id " + id + "not found");
                return BadRequest($"Customer for id {id} not found"); // String interpolation
            }
        }
    }
}
