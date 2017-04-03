using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWithAspNetCore.Models
{
    public class Customer
    {
        public ObjectId Id { get; set; }
        [BsonElement("CustomerId")]
        public int CustomerId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
    }
}
