using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDbWithAspNetCore.Data;
using MongoDbWithAspNetCore.Models;

namespace MongoDbWithAspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;

        //interface must be public to inject
        public CustomersController(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        // GET api/customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetCustomers();
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customerRepository.GetCustomer(id);
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _customerRepository.Remove(id);
        }
    }
}
