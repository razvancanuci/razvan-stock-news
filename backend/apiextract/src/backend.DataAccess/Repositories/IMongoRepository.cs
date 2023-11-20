using backend.DataAccess.Entities;

namespace backend.DataAccess.Repositories;

public interface IMongoRepository
{
    Task<IEnumerable<ApiData>> GetAllDataAsync();
    Task<IEnumerable<ApiData>> GetBySymbolAsync(string symbol);
    Task<ApiData> AddDataAsync(ApiData data);
}