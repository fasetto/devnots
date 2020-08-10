namespace DevNots.Domain
{
    public interface IDbCollection<TAggregate>: IAsyncRepository<TAggregate>
        where TAggregate: AggregateRoot
    {

    }
}
