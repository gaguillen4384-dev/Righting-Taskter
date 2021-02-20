using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace StoriesAccessComponent
{
    /// <summary>
    /// Responsible for acting on the story resource.
    /// </summary>
    public interface IStoriesAccess
    {
        /// <summary>
        /// Retrieves a single story for the given project.
        /// </summary>
        Task<StoryResponse> GetSingleStory(string projectAcronym, int storyNumber);

        /// <summary>
        /// Retrieves all stories for the given project.
        /// </summary>
        Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym);

        /// <summary>
        /// Creates a story for the given project.
        /// </summary>
        Task<StoryResponse> CreateStory(string projectAcronym, StoryCreationRequest storyRequest);

        /// <summary>
        /// Updates a specific story for the given project.
        /// </summary>
        Task<StoryResponse> UpdateStory(string projectAcronym, int storyNumber, StoryUpdateRequest storyRequest);

        /// <summary>
        /// Deletes a specific story for the given project.
        /// </summary>
        Task<bool> DeleteStory(string projectAcronym, int storyNumber);

    }
}
