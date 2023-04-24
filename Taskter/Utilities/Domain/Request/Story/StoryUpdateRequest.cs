using System;
using System.Collections.Generic;

namespace Utilities.Taskter.Domain
{
    public class StoryUpdateRequest
    {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The content of the story.
        /// </summary>
        public IEnumerable<StoryDetail> Details { get; set; } = new List<StoryDetail>();

        /// <summary>
        /// A flag indicating completion of the story.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// A flag indicating if the story its recurrant.
        /// </summary>
        public bool IsRecurrant { get; set; } = false;
    }
}
