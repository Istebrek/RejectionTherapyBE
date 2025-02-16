using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBRejectionTherapyAPI.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string Username { get; set; }
        public List<Dares> CompletedDares { get; set; } = new List<Dares>();
    }
}
