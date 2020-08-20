using DevNots.Domain;

namespace DevNots.Application.Contracts
{
    public class CreateTagRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
