using System;

namespace Utilities.Taskter.Domain
{
    public class StoriesReferenceDetails
    {
        /// <summary>
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

        /// <summary>
        /// Reference to project unique identifier.
        /// </summary>
        // GETTO: change name to Parent ID and add reference type, could be a project could be another story
        public string ProjectId { get; set; } = string.Empty;

        /// <summary>
        /// Reference to story unique identifier.
        /// </summary>
        public string StoryId { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Sequential.
        /// </summary>
        public int StoryNumber { get; set; } = 0;

        /// <summary>
        /// Flag Indicating if its meant to be deleted only set by projectAccess;
        /// </summary>
        public bool IsDeleted { get; set; } = false;

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
