using System.Collections.Generic;

namespace Utilities.Taskter.Domain
{
    /// <summary>
    /// The request to create a story.
    /// </summary>
    public class StoryCreationRequest
    {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Should be sequential.
        /// </summary>
        public int StoryNumber { get; set; } = 0;

        /// <summary>
        /// The content of the story.
        /// </summary>
        public IEnumerable<StoryDetail> Details { get; set; } = new List<StoryDetail>();

        /// <summary>
        /// A flag indicating if the story its recurrant.
        /// </summary>
        public bool IsRecurrant { get; set; } = false;
    }
}
