using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoriesEngine
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesEngine"/>
    /// </summary>
    public class StoriesEngine : IStoriesEngine
    {
        /// <summary>
        /// Concrete implementation of <see cref="IStoriesEngine.CreateStory(StoryDTO)"/>
        /// </summary>
        public Task<string> CreateStory(StoryDTO story)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesEngine.EditStory(StoryDTO)"/>
        /// </summary>
        public Task<StoryDTO> EditStory(StoryDTO story)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesEngine.GetStories(string)"/>
        /// </summary>
        public Task<IList<string>> GetStories(string projectId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesEngine.GetStory(string)"/>
        /// </summary>
        public Task<StoryDTO> GetStory(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
