using backend.DataAccess.Entities;
using backend.DataAccess.Utilities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repository.Impl
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly UserContext _context;
        public Repository(UserContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (typeof(T) == typeof(User))
            {
                await _context.Users.AddAsync((User)(object)entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return default;
        }

        public async Task<T> DeleteByIdAsync(Guid id)
        {
            if (typeof(T) == typeof(User))
            {
                var user = (User)(object)(await _context.Users.ToListAsync()).SingleOrDefault(x => x.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return (T)(object)user;
            }
            return default;

        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            if (typeof(T) == typeof(User))
            {
                return (T)(object)(await _context.Users.ToListAsync()).SingleOrDefault(x => x.Id == id);
            }
            return default;
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            if (typeof(T) == typeof(User))
            {
                return (IEnumerable<T>) await _context.Users.ToListAsync();
            }
            return default;
        }

        public async Task UpdateCode(Guid id, string code)
        {
            if (typeof(T) == typeof(Admin))
            {
                var admin  = (await _context.Admins.ToListAsync()).SingleOrDefault(a => a.UserId == id);
                admin.TwoFactorCode = code;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T?> GetByGuidAndStringAsync(Guid id, string code)
        {
            if (typeof(T) == typeof(Admin))
            {
                var admin  = (await _context.Admins.Include(a => a.User).ToListAsync())
                    .SingleOrDefault(a => a.UserId == id && a.TwoFactorCode == code);
                if (admin == null)
                {
                    return null;
                }

                return (T)(object)admin;
            }

            return default;
        }

        public async Task<T?> GetByAStringAsync(string prop)
        {
            if (typeof(T) == typeof(User))
            {
                return (T)(object)(await _context.Users.ToListAsync()).SingleOrDefault(x => x.Username == prop);
            }

            if (typeof(T) == typeof(Admin))
            {
                var user = (await _context.Users.ToListAsync()).SingleOrDefault(x => x.Username == prop);
                if (user == null)
                {
                    return null;
                }
                var admin = (await _context.Admins.ToListAsync()).SingleOrDefault(x => x.UserId == user.Id);
                return (T)(object) admin;
            }
                
            return default;
        }

        public async Task<T?> GetByStringAndPasswordAsync(string prop1, string pass)
        {
            if (typeof(T) == typeof(User))
            {
                var passhash = Crypto.Encrypt(pass);
                return (T)(object)(await _context.Users.ToListAsync()).SingleOrDefault(x => x.Username == prop1 && x.Password == passhash);
            }
            return default;
        }
    }
}
