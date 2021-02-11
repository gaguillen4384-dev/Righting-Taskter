using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace StoriesAccessComponent.Repositories
{
    public interface IStoriesAccess
    {
        /// <summary>
        /// Retrieves a single project for the given project.
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

        // TODO: Need an update function that automates the completion and update information at the resouceaccess
    }
}
