using System;
using System.Collections.Generic;

namespace DevNots.Domain
{
    public class Note: AggregateRoot, INoteDetails
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public DateTime CreatedAt { get; set; }

        public Note(
            string userId,
            string title,
            string text,
            string description = default
        )
        {
            this.UserId      = userId;
            this.Text        = text;
            this.Title       = title;
            this.Description = description;
        }
    }
}
