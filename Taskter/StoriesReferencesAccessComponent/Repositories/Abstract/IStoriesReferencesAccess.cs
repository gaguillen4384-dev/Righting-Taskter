using LiteDB;
using System.Collections.Generic;
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
        Task MakeStoriesReferenceForProject(string projectAcronym, string projectId);

        /// <summary>
        /// Creates a story reference for the given project.
        /// </summary>
        Task MakeReferenceForProjectAndStory(string projectAcronym, int storyNumber, ObjectId storyId, ObjectId projectId);

        /// <summary>
        /// Gets the story id from a story reference.
        /// </summary>
        Task<string> GetSingleStoryId(string projectAcronym, int storyNumber);

        /// <summary>
        /// Gets the story ids from stories references.
        /// </summary>
        Task<IEnumerable<string>> GetProjectStoriesIds(string projectAcronym);

        /// <summary>
        /// Gets the project id from stories references.
        /// </summary>
        Task<ObjectId> GetProjectId(string projectAcronym);

        /// <summary>
        /// Updates a story reference project acronym.
        /// </summary>
        Task UpdateStoryReferenceAcronym(string updateProjectAcronym, string projectId);

        /// <summary>
        /// Deletes a story reference. If could not delete, returns false.
        /// </summary>
        Task<bool> RemoveReferenceOfStory(string storyId);
    }
}
