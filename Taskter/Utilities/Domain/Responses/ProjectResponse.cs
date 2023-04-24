using System;

namespace Utilities.Taskter.Domain
{
    public class ProjectResponse
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// Last date the project got worked on.
        /// </summary>
        public DateTime LastWorkedOn = DateTime.UtcNow;

        /// <summary>
        /// The date the project got created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// The date the project got updated.
        /// </summary>
        public DateTime? DateUpdated { get; set; } = null;

        /// <summary>
        /// The number of active stories in the project.
        /// </summary>
        // TODO: Theres a need for having the IDS not just the count of active.
        public int NumberOfActiveStories = 0;

        /// <summary>
        /// The number of completed stories in the project.
        /// </summary>
        public int NumberOfCompletedStories = 0;

        /// <summary>
        /// The project acronym, used as an identifier.
        /// </summary>
        public string ProjectAcronym = string.Empty;

    }
}
