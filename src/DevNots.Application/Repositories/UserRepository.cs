using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevNots.Domain;

namespace DevNots.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext dbContext;
        public UserRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<string> CreateAsync(User aggregate)
        {
            return dbContext.Users.CreateAsync(aggregate);
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return dbContext.Users.FindAsync(predicate);
        }

        public Task<User> FindOneAsync(Expression<Func<User, bool>> predicate)
        {
            return dbContext.Users.FindOneAsync(predicate);
        }

        public Task<User> GetByIdAsync(string id)
        {
            return dbContext.Users.GetByIdAsync(id);
        }

        public Task<IEnumerable<User>> PaginateAsync(int page, int pageSize)
        {
            return dbContext.Users.PaginateAsync(page, pageSize);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return dbContext.Users.RemoveAsync(id);
        }

        public Task<bool> UpdateAsync(string id, User aggregate)
        {
            return dbContext.Users.UpdateAsync(id, aggregate);
        }
    }
}
