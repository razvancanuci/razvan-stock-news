using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repository.Impl
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly SubscriberContext _context;
        public Repository(SubscriberContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                await _context.Subscribers.AddAsync((Subscriber)(object)entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            if (typeof(T) == typeof(Answer))
            {
                await _context.Answers.AddAsync((Answer)(object)entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            
            return default;
        }

        public async Task<T> DeleteByAStringAsync(string email)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                var subscriber = (await _context.Subscribers.ToListAsync()).SingleOrDefault(x => x.Email == email);
                var answer = _context.Answers.SingleOrDefault(x => x.SubscriberId == subscriber.Id);
                if (answer != null)
                {
                    _context.Answers.Remove(answer);
                }
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return (T)(object)subscriber;
            }
            return default;
        }

        public async Task<T> DeleteByGuidAsync(Guid id)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                var subscriber = (await _context.Subscribers.ToListAsync()).SingleOrDefault(x => x.Id == id);
                var answer = _context.Answers.SingleOrDefault(x => x.SubscriberId == subscriber.Id);
                if (answer != null)
                {
                    _context.Answers.Remove(answer);
                }
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
                return (T)(object)subscriber;
            }
            return default;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Subscriber))
            {
                return (IEnumerable<T>)await _context.Subscribers.ToListAsync();
            }
            return default;
        }

        public async Task<T?> GetByGuidAsync(Guid id)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                return (T)(object)(await _context.Subscribers.Include(x => x.Answer).ToListAsync()).SingleOrDefault(x => x.Id == id);
            }

            if (typeof(T) == typeof(Answer))
            {
                return (T)(object) _context.Answers.SingleOrDefault(x => x.SubscriberId == id);
            }
            return default;
        }

        public async Task<T?> GetByStringAsync(string email)
        {
            if (typeof(T) == typeof(Subscriber))
            {
                return (T)(object)(await _context.Subscribers.Include(x => x.Answer).ToListAsync()).SingleOrDefault(x => x.Email == email);
            }
            return default;
        }
    }

}
