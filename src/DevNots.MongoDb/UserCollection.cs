using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevNots.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevNots.MongoDb
{
    public class UserCollection : IDbCollection<User>
    {
        private IMongoCollection<User> collection;
        public UserCollection(IMongoCollection<User> collection)
        {
            this.collection = collection;
        }

        public async Task<string> CreateAsync(User candidate)
        {
            await collection.InsertOneAsync(candidate).ConfigureAwait(false);
            return candidate.Id;
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await collection.FindAsync(predicate).ConfigureAwait(false);
            return users.ToList();
        }

        public async Task<User> FindOneAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await collection.FindAsync(predicate).ConfigureAwait(false);
            return users.FirstOrDefault();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var users = await collection.FindAsync(x => x.Id == id).ConfigureAwait(false);
            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> PaginateAsync(int page, int pageSize)
        {
            var users = collection.Aggregate()
                .Match(FilterDefinition<User>.Empty)
                .SortByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize);

            return await users.ToListAsync();
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await  collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAsync(string id, User aggregate)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await collection.ReplaceOneAsync(x => x.Id == id, aggregate);
            return result.IsModifiedCountAvailable;
        }
    }
}
