namespace DevNots.Application.Contracts
{
    public class AddNoteRequest
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }

    }
}
