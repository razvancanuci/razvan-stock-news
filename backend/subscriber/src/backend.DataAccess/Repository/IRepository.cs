using backend.DataAccess.Entities;

namespace backend.DataAccess.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByStringAsync(string email);
        Task<T?> GetByGuidAsync(Guid id);
        Task<T> DeleteByGuidAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> DeleteByAStringAsync(string email);

    }
}
