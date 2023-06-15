using LiteDbDriver;

namespace StoriesReferencesAccessComponent
{
    /// <summary>
    /// Responsible for keeping a relationship between stories and projects, using project acronym.
    /// </summary>
    public class StoryReferenceDocument : BaseDocument
    {
        /// <summary>
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

        /// <summary>
        /// Reference to project unique identifier.
        /// </summary>
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

        [BsonConstructor]
        public StoryReferenceDocument() : base() { }

    }
}
