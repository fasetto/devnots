using System;
using DevNots.Domain;

namespace DevNots.Application.Contracts
{
    public class TagResponse: ITagDetails
    {
        public string Id { get ; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
