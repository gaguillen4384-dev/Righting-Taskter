using System;

namespace Utilities.Taskter.Domain
{
    public class ProjectMetadataDetails
    {
        /// <summary>
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Sequential.
        /// </summary>
        public int LatestStoryNumber { get; set; } = 0;

        /// <summary>
        /// The number of stories completed in the project. Sequential.
        /// </summary>
        public int NumberOfStoriesCompleted { get; set; } = 0;

        /// <summary>
        /// The number of active stories in the project.
        /// </summary>
        public int NumberOfActiveStories { get; set; } = 0;

        /// <summary>
        /// The date the object got created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date the object got updated.
        /// </summary>
        public DateTime? DateUpdated { get; set; } = null;
    }
}
