using MongoDB.Bson;
using MongoDbWithAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWithAspNetCore.Data
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        Customer Create(Customer customer);
        void Update(int id, Customer customer);
        void Remove(int id);
    }
}
