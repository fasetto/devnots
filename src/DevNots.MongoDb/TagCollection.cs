using DevNots.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Tag = DevNots.Domain.Tag;

namespace DevNots.MongoDb
{
    public class TagCollection: IDbCollection<Tag>
    {
        private readonly IMongoCollection<Tag> collection;
        public TagCollection(IMongoCollection<Tag> collection)
        {
            this.collection = collection;
        }

        public async Task<Tag> GetByIdAsync(string id)
        {
            var tag = await collection.FindAsync(x => x.Id == id).ConfigureAwait(false);
            return tag.FirstOrDefault();
        }

        public async Task<IEnumerable<Tag>> PaginateAsync(int page, int pageSize)
        {
            var tags = collection.Aggregate()
                .Match(FilterDefinition<Tag>.Empty)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize);

            return await tags.ToListAsync();
        }

        public async Task<string> CreateAsync(Tag aggregate)
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

        public async Task<bool> UpdateAsync(string id, Tag aggregate)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await collection.ReplaceOneAsync(x => x.Id == id, aggregate);
            return result.IsModifiedCountAvailable;
        }

        public async Task<IEnumerable<Tag>> FindAsync(Expression<Func<Tag, bool>> predicate)
        {
            var tags = await collection.FindAsync(predicate).ConfigureAwait(false);
            return tags.ToList();
        }

        public async Task<Tag> FindOneAsync(Expression<Func<Tag, bool>> predicate)
        {
            var tags = await collection.FindAsync(predicate).ConfigureAwait(false);
            return tags.FirstOrDefault();
        }
    }
}
