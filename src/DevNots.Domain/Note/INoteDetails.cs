using System;
using System.Collections.Generic;

namespace DevNots.Domain
{
    public interface INoteDetails
    {
        string UserId { get; }
        string Text { get; }
        string Title { get; }
        string Description { get; }
        DateTime CreatedAt { get; }
        IEnumerable<Tag> TagList { get; }
    }
}
