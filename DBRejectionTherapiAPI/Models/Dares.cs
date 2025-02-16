using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBRejectionTherapyAPI.Models
{
    public class Dares
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public string Name { get; set; }
        public string Dare {  get; set; }
    }
}
