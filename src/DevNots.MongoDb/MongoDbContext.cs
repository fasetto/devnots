using DevNots.Domain;
using MongoDB.Driver;

namespace DevNots.MongoDb
{
    public class MongoDbContext: DbContext
    {
        public override IDbCollection<User> Users => new UserCollection(database.GetCollection<User>("Users"));

        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoDbContext(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);

            client = new MongoClient(mongoUrl);
            database = client.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
