using LaconicsCrm.webapi.Data;
using LaconicsCrm.webapi.Models.Domain;
using LaconicsCrm.webapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaconicsCrm.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly LaconicsDatabaseContext laconicsDatabaseContext;
        private readonly ICustomerRepository customerRepository;

        public CustomerController(LaconicsDatabaseContext laconicsDatabaseContext, ICustomerRepository customerRepository)
        {
            this.laconicsDatabaseContext = laconicsDatabaseContext;
            this.customerRepository = customerRepository;
        }

        //get all customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customerDomain = await customerRepository.GetAllAsync(); //get data from database
            return Ok(customerDomain);
        }

        //get customer by id (single customer)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            // var customer = laconicsDatabaseContext.Customers.Find(id);
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //create new customer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var customerModel = new Customer
            {
                firstname = customer.firstname,
                lastname = customer.lastname,
                email = customer.email
            };
            // laconicsDatabaseContext.Customers.Add(customerModel);
            customerModel = await customerRepository.CreateAsync(customerModel);
            laconicsDatabaseContext.SaveChanges();

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = customerModel.id }, customerModel);
            return Ok(customerModel);

        }

        //update cutomer
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Customer customer)
        {
            //var customerModel = laconicsDatabaseContext.Customers.FirstOrDefault(x => x.id == id);

            var customerModel = new Customer
            {
                firstname = customer.firstname,
                lastname = customer.lastname,
                email = customer.email
            };

            customerModel = await customerRepository.UpdateAsync(id, customer);

            if (customerModel == null)
            {
                return NotFound();
            }
            //customerModel.firstname = customer.firstname;
            //customerModel.lastname = customer.lastname;
            //customerModel.email = customer.email;

            //laconicsDatabaseContext.SaveChanges();

            return Ok(customerModel);
        }

        //delete customer
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // var customerModel = laconicsDatabaseContext.Customers.FirstOrDefault(x => x.id == id);
            var customerModel = await customerRepository.DeleteAsync(id);

            if (customerModel == null)
            {
                return NotFound();
            }
            //delete customer
            //laconicsDatabaseContext.Customers.Remove(customerModel);
            //laconicsDatabaseContext.SaveChanges();
            return Ok(customerModel);
        }

    }
}
