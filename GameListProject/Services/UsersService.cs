
using GameListProject.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UserStoreApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _UsersCollection;

    public UsersService(
        IOptions<UserDatabaseSettings> UserStoreDatabaseSettings)

    {
        var mongoClient = new MongoClient(
            UserStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            UserStoreDatabaseSettings.Value.DatabaseName);

        _UsersCollection = mongoDatabase.GetCollection<User>(
            UserStoreDatabaseSettings.Value.UsersCollectionName);


    }

    public async Task<List<User>> GetAsync() =>
        await _UsersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _UsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _UsersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _UsersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _UsersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<User?> GetAsyncByName(string nome) =>
        await _UsersCollection.Find(x => x.Nome == nome).FirstOrDefaultAsync();

}