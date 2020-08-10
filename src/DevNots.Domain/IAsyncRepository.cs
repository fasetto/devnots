using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevNots.Domain
{
    public interface IAsyncRepository<TAggregate>
        where TAggregate: AggregateRoot
    {
        Task<TAggregate> GetByIdAsync(string id);
        Task<IEnumerable<TAggregate>> PaginateAsync(int page, int pageSize);
        Task<string> CreateAsync(TAggregate aggregate);
        Task<bool> RemoveAsync(string id);
        Task<bool> UpdateAsync(string id, TAggregate aggregate);
        Task<IEnumerable<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> predicate);
    }
}
