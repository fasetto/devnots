using System.Collections.Generic;
using DevNots.Domain;

namespace DevNots.Application.Contracts
{
    public class UpdateNoteRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> TagList { get; set; }
    }
}
