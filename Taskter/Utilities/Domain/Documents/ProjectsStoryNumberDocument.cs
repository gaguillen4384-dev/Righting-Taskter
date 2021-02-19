using LiteDbDriver;

namespace Utilities.Taskter.Domain.Documents
{
    public class ProjectsStoryNumberDocument : BaseDocument
    {
        /// <summary>
        /// The Acronym of the project.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

        /// <summary>
        /// The number of the story in the project its on. Sequential.
        /// </summary>
        public int LatestStoryNumber { get; set; } = 0;
    }
}
