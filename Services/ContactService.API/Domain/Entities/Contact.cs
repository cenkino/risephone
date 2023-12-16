using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactService.API.Domain.Entities
{
    public class Contact
    {
        public Contact(string name, string lastName, string company)
        {
            Name = name;
            LastName = lastName;
            Company = company;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
