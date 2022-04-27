using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameListProject.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; } = null!;
        [BsonElement("Genero")]
        public string Senha { get; set; } = null!;
        [BsonElement("Descricao")]
        public string Descricao { get; set; } = null!;
        [BsonElement("Avaliacao")]
        public float Avaliacao { get; set; }
        [BsonElement("Image")]
        public string Image { get; set; } = null!;

    }
}
