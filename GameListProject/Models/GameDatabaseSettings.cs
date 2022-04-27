namespace GameListProject.Models
{
    public class GameDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://chuckbabyReal:rudneygay123@gamelist.fkndq.mongodb.net/test";

        public string DatabaseName { get; set; } = "GameList";

        public string GamesCollectionName { get; set; } = "Games";
    }
}
