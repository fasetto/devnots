using DevNots.Domain;
using MongoDB.Driver;
using Tag = DevNots.Domain.Tag;

namespace DevNots.MongoDb
{
    public class MongoDbContext: DbContext
    {
        public override IDbCollection<User> Users => new UserCollection(database.GetCollection<User>("Users"));
        public override IDbCollection<Note> Notes => new NoteCollection(database.GetCollection<Note>("Notes"));
        public override IDbCollection<Tag> Tags => new TagCollection(database.GetCollection<Tag>("Tags"));

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
