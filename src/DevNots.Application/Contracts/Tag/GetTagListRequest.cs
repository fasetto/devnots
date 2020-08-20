namespace DevNots.Application.Contracts
{
    public class GetTagListRequest
    {
        public string UserId { get; set; }
        public int Limit { get; set; } = 20;
    }
}
