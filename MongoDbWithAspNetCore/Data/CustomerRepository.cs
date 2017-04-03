using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDbWithAspNetCore.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDbWithAspNetCore.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        MongoClient _client;
        IMongoDatabase _db;

        public CustomerRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("CustomerDB");
        }
        public Customer Create(Customer customer)
        {
             _db.GetCollection<Customer>("Customers").InsertOne(customer);
            return customer;
        }

        public Customer GetCustomer(int id)
        {
            return _db.GetCollection<Customer>("Customers").Find(c => c.CustomerId == id).FirstOrDefault();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _db.GetCollection<Customer>("Customers").Find(_ => true).ToEnumerable();
        }

        public void Remove(int id)
        {
            _db.GetCollection<Customer>("Customers").DeleteOne(c => c.CustomerId == id);
        }

        public void Update(int id, Customer customer)
        {
            _db.GetCollection<Customer>("Customers").ReplaceOne(c => c.CustomerId == id, customer);
        }
    }
}
