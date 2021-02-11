using System;
using System.Collections.Generic;

namespace Utilities.Domain
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
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

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
