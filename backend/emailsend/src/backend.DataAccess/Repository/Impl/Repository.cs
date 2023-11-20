using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repository.Impl
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly EmailContext _context;
        public Repository(EmailContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                _context.Subscribers.Add((Subscriber)(object)entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return default;
        }

        public async Task<T> DeleteByGuid(Guid id)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                var subscriber = (await _context.Subscribers.ToListAsync()).SingleOrDefault(x => x.Id == id);
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return (T)(object)subscriber;

            }
            return default;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Subscriber))
            {
                return (IEnumerable<T>)await _context.Subscribers.ToListAsync();

            }
            return default;
        }
    }
}
