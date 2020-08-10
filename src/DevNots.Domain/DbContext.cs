namespace DevNots.Domain
{
    public abstract class DbContext
    {
        public abstract IDbCollection<User> Users { get; }
    }
}
