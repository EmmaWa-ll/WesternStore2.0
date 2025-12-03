using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WesternStore2._0.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsAdmin { get; set; }

    }
}
