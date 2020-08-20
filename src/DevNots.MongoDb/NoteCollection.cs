using DevNots.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DevNots.MongoDb
{
    public class NoteCollection:IDbCollection<Note>
    {
        private readonly IMongoCollection<Note> collection;
        public NoteCollection(IMongoCollection<Note> collection)
        {
            this.collection = collection;
        }

        public async Task<Note> GetByIdAsync(string id)
        {
            var note = await collection.FindAsync(x => x.Id == id).ConfigureAwait(false);
            return note.FirstOrDefault();
        }

        public async Task<IEnumerable<Note>> PaginateAsync(int page, int pageSize)
        {
            var notes = collection.Aggregate()
                .Match(FilterDefinition<Note>.Empty)
                .SortByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize);

            return await notes.ToListAsync();
        }

        public async Task<string> CreateAsync(Note aggregate)
        {
            await collection.InsertOneAsync(aggregate).ConfigureAwait(false);
            return aggregate.Id;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAsync(string id, Note aggregate)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var note = await collection.ReplaceOneAsync(x => x.Id == id, aggregate);
            return note.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Note>> FindAsync(Expression<Func<Note, bool>> predicate)
        {
            var notes = await collection.FindAsync(predicate).ConfigureAwait(false);
            return notes.ToList();
        }

        public async Task<Note> FindOneAsync(Expression<Func<Note, bool>> predicate)
        {
            var notes = await collection.FindAsync(predicate).ConfigureAwait(false);
            return notes.FirstOrDefault();
        }
    }
}
