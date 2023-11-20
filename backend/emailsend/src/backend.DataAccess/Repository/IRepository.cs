using backend.DataAccess.Entities;

namespace backend.DataAccess.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<T> DeleteByGuid(Guid id);

        Task<IEnumerable<T>> GetAll();
    }
}
