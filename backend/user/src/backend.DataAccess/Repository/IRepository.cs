using backend.DataAccess.Entities;

namespace backend.DataAccess.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        public Task<T> CreateAsync(T entity);
        public Task<T?> GetByAStringAsync(string prop);
        public Task<T?> GetByStringAndPasswordAsync(string prop1, string pass);
        public Task<T> DeleteByIdAsync(Guid id);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>?> GetAllAsync();
        public Task UpdateCode(Guid id, string code);

        public Task<T?> GetByGuidAndStringAsync(Guid id, string code);
    }
}
