using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesAccessProxy">
    /// </summary>
    public class StoriesAccessProxy : IStoriesAccessProxy
    {
        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.CreateStory(string, StoryRequest)">
        /// </summary>
        public Task<StoryResponse> CreateStory(string projectAcronym, StoryRequest storyRequest)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.GetProjectStories(string)">
        /// </summary>
        public Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.GetSingleStory(string, string)">
        /// </summary>
        Task<StoryResponse> IStoriesAccessProxy.GetSingleStory(string projectAcronym, string storyIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}
