using System.Threading.Tasks;

namespace StoriesReferencesAccessComponent
{
    /// <summary>
    /// Responsible for managing the stories references.
    /// </summary>
    public interface IStoriesReferencesAccess
    {
        /// <summary>
        /// Create a story reference for a project.
        /// </summary>
        Task CreateStoryReferenceForProject(string projectAcronym, string projectId);

        /// <summary>
        /// Updates a story reference project acronym.
        /// </summary>
        Task UpdateStoryReferenceAcronym(string updateProjectAcronym, string projectId);
    }
}
