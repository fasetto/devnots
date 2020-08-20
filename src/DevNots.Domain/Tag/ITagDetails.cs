using System;

namespace DevNots.Domain
{
    public interface ITagDetails
    {
        string UserId { get; }
        string Name { get; }
        string Color { get; }
        DateTime CreatedAt { get; }
    }
}
