using backend.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.DataAccess.Repositories.Impl;

public class MongoRepository : IMongoRepository
{
    private readonly IMongoCollection<ApiData> _collection;
    
    private static readonly FilterDefinitionBuilder<ApiData> FilterBuilder = Builders<ApiData>.Filter;
    
    public MongoRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        IMongoDatabase database=mongoClient.GetDatabase(configuration["Database:DbName"]);
        _collection=database.GetCollection<ApiData>(configuration["Database:DbCollection"]);   
    }
    
    public async Task<IEnumerable<ApiData>> GetAllDataAsync()
    {
        return await  _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<IEnumerable<ApiData>> GetBySymbolAsync(string symbol)
    {
        var filter = FilterBuilder.Eq(data => data.Symbol, symbol);
        var data = await _collection.Find(filter).ToListAsync();

        return data;
    }

    public async Task<ApiData> AddDataAsync(ApiData data)
    {
        await _collection.InsertOneAsync(data);
        return data;
    }
}