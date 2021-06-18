using LiteDbDriver;
using System;
using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace StoriesAccessComponent
{
    public class StoryDocument : BaseDocument
    {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Sequential.
        /// </summary>
        public int StoryNumber { get; set; } = 0;

        /// <summary>
        /// The content of the story.
        /// </summary>
        public IEnumerable<StoryDetail> Details { get; set; } = new List<StoryDetail>();

        /// <summary>
        /// The date the story got completed.
        /// </summary>
        public DateTime? DateCompleted { get; set; } = null;

        /// <summary>
        /// A flag indicating completion of the story.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// A flag indicating if the story its recurrant.
        /// </summary>
        public bool IsRecurrant { get; set; } = false;

        [BsonConstructor]
        public StoryDocument() : base() { }
    }
}
