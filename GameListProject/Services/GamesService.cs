
using GameListProject.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameStoreApi.Services;

public class GamesService
{
    private readonly IMongoCollection<Game> _GamesCollection;

    public GamesService(
        IOptions<GameDatabaseSettings> GameStoreDatabaseSettings)

    {
        var mongoClient = new MongoClient(
            GameStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            GameStoreDatabaseSettings.Value.DatabaseName);

        _GamesCollection = mongoDatabase.GetCollection<Game>(
            GameStoreDatabaseSettings.Value.GamesCollectionName);

    }

    public async Task<List<Game>> GetAsync() =>
        await _GamesCollection.Find(_ => true).ToListAsync();

    public async Task<Game?> GetAsync(string id) =>
        await _GamesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Game newGame) =>
        await _GamesCollection.InsertOneAsync(newGame);

    public async Task UpdateAsync(string id, Game updatedGame) =>
        await _GamesCollection.ReplaceOneAsync(x => x.Id == id, updatedGame);

    public async Task RemoveAsync(string id) =>
        await _GamesCollection.DeleteOneAsync(x => x.Id == id);
}