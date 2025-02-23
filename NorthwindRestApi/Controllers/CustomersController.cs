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


        //Hakee asiakkaan pääavaimella 
        [HttpGet("{id}")]
        // or [Route("{id}")]
        public ActionResult GetOneCustomersById(string id)
        {
            try
            {
                var customer = db.Customers.Find(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    //return BadRequest("Customer for that id " + id + "not found");
                    return NotFound($"Customer for id: {id} not found"); // String interpolation
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Uuden lisaaminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok($"Lisataan uusi asiakas {cust.CompanyName} from {cust.City}");
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue Lisaa: " + e.InnerException);
            }

        }
    }
}
