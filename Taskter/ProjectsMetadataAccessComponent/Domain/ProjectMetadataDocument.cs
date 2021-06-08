using LiteDB;
using LiteDbDriver;

namespace ProjectsMetadataAccessComponent
{
    public class ProjectMetadataDocument : BaseDocument
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

        [BsonCtor]
        public ProjectMetadataDocument() : base() { }

    }
}
