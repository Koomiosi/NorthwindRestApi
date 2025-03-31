using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Aluastetaan tiettokanta yhteys
        //NorthwindOriginalContext db = new NorthwindOriginalContext();
        // or NorthwindOriginalContext db =  new();

        // Dependency Injektio
        private NorthwindOriginalContext db;

        public CustomersController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }


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

        //Asiakkaan poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok("Asiakas " + asiakas.CompanyName + " poistettiin.");
                }
                else
                {
                    return NotFound("Asiakasta id:lla " + id + "ei loydy.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // Asiakkaan muokkaaminen 

        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer customer)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    asiakas.CompanyName = customer.CompanyName;
                    asiakas.ContactName = customer.ContactName;
                    asiakas.Address = customer.Address;
                    asiakas.City = customer.City;
                    asiakas.Region = customer.Region;
                    asiakas.PostalCode = customer.PostalCode;
                    asiakas.Country = customer.Country;
                    asiakas.Phone = customer.Phone;
                    asiakas.Fax = customer.Fax;

                    db.SaveChanges();
                    return Ok("Muokattu asiakasta " + asiakas.CompanyName);
                }
                else
                {
                    return NotFound("Asiakasta ei loytynyt." + id);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }           
        }

        // Haku nimen osalla
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname) 
        {
            try
            {
                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname));
                
                //var cust = from c in db.Customers where c.CompanyName.Contains(cname) select c; <-- Sama kuin ylhaalla mutta normi linq kysely
                
                //var cust =db,Customers.Where(c => c.CompanyName == cname); <--  nimi taytyy olla taysin oikein
                
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}