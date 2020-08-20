using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevNots.Domain;

namespace DevNots.Application.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DbContext dbContext;

        public TagRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<string> CreateAsync(Tag aggregate)
        {
            return dbContext.Tags.CreateAsync(aggregate);
        }

        public Task<IEnumerable<Tag>> FindAsync(Expression<Func<Tag, bool>> predicate)
        {
            return dbContext.Tags.FindAsync(predicate);
        }

        public Task<Tag> FindOneAsync(Expression<Func<Tag, bool>> predicate)
        {
            return dbContext.Tags.FindOneAsync(predicate);
        }

        public Task<Tag> GetByIdAsync(string id)
        {
            return dbContext.Tags.GetByIdAsync(id);
        }

        public Task<IEnumerable<Tag>> PaginateAsync(int page, int pageSize)
        {
            return dbContext.Tags.PaginateAsync(page, pageSize);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return dbContext.Tags.RemoveAsync(id);
        }

        public Task<bool> UpdateAsync(string id, Tag aggregate)
        {
            return dbContext.Tags.UpdateAsync(id, aggregate);
        }
    }
}
