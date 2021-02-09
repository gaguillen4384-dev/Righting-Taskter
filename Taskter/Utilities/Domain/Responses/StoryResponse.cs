using System;

namespace Utilities.Domain
{
    public class StoryResponse
    {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronymName { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Sequential.
        /// </summary>
        public string StoryNumber { get; set; } = string.Empty;

        /// <summary>
        /// The content of the story.
        /// </summary>
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// The date the story got created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// The date the story got updated.
        /// </summary>
        public DateTime? DateUpdated { get; set; } = null;

        /// <summary>
        /// The date the story got completed.
        /// </summary>
        public DateTime? DateCompleted { get; set; } = null;

        /// <summary>
        /// A flag indicating completion of the story.
        /// </summary>
        public bool Completed { get; set; } = false;

        /// <summary>
        /// A flag indicating if the story its recurrant.
        /// </summary>
        public bool Recurrant { get; set; } = false;

        /// <summary>
        /// A counter of Pomodoros acted for this story.
        /// </summary>
        public int PcCounter { get; set; } = 0;
    }
}
