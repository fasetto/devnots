using System.Collections.Generic;

namespace DevNots.Domain
{
    public interface INoteDetails
    {
        string UserId { get; }
        string Text { get; }
        string Title { get; }
        string Description { get; }
        IEnumerable<string> Keywords { get; }

    }
}
