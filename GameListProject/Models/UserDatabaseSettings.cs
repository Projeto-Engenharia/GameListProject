namespace GameListProject.Models
{
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://chuckbabyReal:rudneygay123@gamelist.fkndq.mongodb.net/test";

        public string DatabaseName { get; set; } = "GameList";

        public string UsersCollectionName { get; set; } = "Users";
    }
}
