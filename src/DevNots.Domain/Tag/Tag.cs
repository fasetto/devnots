using System;

namespace DevNots.Domain
{
    public class Tag: AggregateRoot, ITagDetails
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }

        public Tag(string userId, string name, string color)
        {
            UserId = userId;
            Name   = name;
            Color  = color;
        }
    }
}
