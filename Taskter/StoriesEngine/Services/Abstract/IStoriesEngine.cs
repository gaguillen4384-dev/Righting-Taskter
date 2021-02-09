using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace StoriesEngine
{
    /// <summary>
    /// Responsible for encapsulating Stories Management changes
    /// </summary>
    interface IStoriesEngine
    {
        /// <summary>
        /// Creates a story.
        /// </summary>
        /// <returns>The story ID</returns>
        Task<string> CreateStory(StoryRequest story);

        /// <summary>
        /// Get ALL stories ids of a project.
        /// </summary>
        Task<IList<string>> GetStories(string projectId);

        /// <summary>
        /// Get a project.
        /// </summary>
        Task<StoryResponse> GetStory(string projectId, string storyIdentifier);

        /// <summary>
        /// Edit Story: atomic operations on the lines of story and story own metadata.
        /// </summary>  
        /// <returns>The story ID</returns>
        Task<StoryResponse> EditStory(StoryRequest story);
    }
}
