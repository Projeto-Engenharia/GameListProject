using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameListProject.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
            
        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
        [BsonElement("Senha")]
        public string Senha { get; set; } = null!;
        public List<Game> Games { get; set; } = null!;

    }
}
