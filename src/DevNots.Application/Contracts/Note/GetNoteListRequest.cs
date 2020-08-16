namespace DevNots.Application.Contracts
{
    public class GetNoteListRequest
    {
        public string UserId { get; set; }
        public int Limit { get; set; } = 20;
    }
}
