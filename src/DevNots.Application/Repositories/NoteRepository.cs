using DevNots.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevNots.Application.Repositories
{
    public class NoteRepository:INoteRepository
    {
        private readonly DbContext context;

        public NoteRepository(DbContext context)
        {
            this.context = context;
        }

        public Task<Note> GetByIdAsync(string id)
        {
            return context.Notes.GetByIdAsync(id);
        }

        public Task<IEnumerable<Note>> PaginateAsync(int page, int pageSize)
        {
            return context.Notes.PaginateAsync(page, pageSize);
        }

        public Task<string> CreateAsync(Note aggregate)
        {
            return context.Notes.CreateAsync(aggregate);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return context.Notes.RemoveAsync(id);
        }

        public Task<bool> UpdateAsync(string id, Note aggregate)
        {
            return context.Notes.UpdateAsync(id, aggregate);
        }

        public Task<IEnumerable<Note>> FindAsync(Expression<Func<Note, bool>> predicate)
        {
            return context.Notes.FindAsync(predicate);

        }

        public Task<Note> FindOneAsync(Expression<Func<Note, bool>> predicate)
        {
            return context.Notes.FindOneAsync(predicate);
        }
    }
}
