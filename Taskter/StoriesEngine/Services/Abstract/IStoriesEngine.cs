using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<string> CreateStory(StoryDTO story);

        /// <summary>
        /// Get ALL stories ids of a project.
        /// </summary>
        Task<IList<string>> GetStories(string projectId);

        /// <summary>
        /// Get a project.
        /// </summary>
        Task<StoryDTO> GetStory(string id);

        /// <summary>
        /// Edit Story: atomic operations on the lines of story and story own metadata.
        /// </summary>  
        /// <returns>The story ID</returns>
        Task<StoryDTO> EditStory(StoryDTO story);
    }
}
