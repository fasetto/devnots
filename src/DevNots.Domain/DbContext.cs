namespace DevNots.Domain
{
    public abstract class DbContext
    {
        public abstract IDbCollection<User> Users { get; }
        public abstract IDbCollection<Note> Notes { get; }
        public abstract IDbCollection<Tag> Tags { get; }
    }
}
