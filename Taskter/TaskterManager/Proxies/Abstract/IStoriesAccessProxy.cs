using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    public interface IStoriesAccessProxy
    {
        /// <summary>
        /// Retrieves a single project for the given project.
        /// </summary>
        Task<StoryResponse> GetSingleStory(string projectAcronym, string storyIdentifier);

        /// <summary>
        /// Retrieves all stories for the given project.
        /// </summary>
        Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym);


        /// <summary>
        /// Creates a story for the given project.
        /// </summary>
        Task<StoryResponse> CreateStory(string projectAcronym, StoryCreationRequest storyRequest);
    }
}
