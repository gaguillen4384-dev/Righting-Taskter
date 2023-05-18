using StoriesAccessComponent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesAccessProxy">
    /// </summary>
    public class StoriesAccessProxy : IStoriesAccessProxy
    {
        private IStoriesAccess _storiesAccess;

        public StoriesAccessProxy(IStoriesAccess projectConnection)
        {
            _storiesAccess = projectConnection;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.ReadMultipleStories">
        /// </summary>
        public async Task<IEnumerable<StoryResponse>> ReadMultipleStories(IEnumerable<string> storiesID)
        {
            return await _storiesAccess.ReadMultipleStories(storiesID);
        }

        //GETTO: this is a service not proxy thing. requires two different RA
        ///// <summary>
        ///// Concrete implementation of <see cref="IStoriesAccessProxy.ReadStoriesForAProject">
        ///// </summary>
        //public async Task<IEnumerable<StoryResponse>> ReadStoriesForAProject(string projectAcronym)
        //{
        //    //    // use stories ID list, filter to find all stories
        //    var listOfStoriesID = await _storiesAccess.GetProjectStoriesIds(projectAcronym);
        //    var result = await _storiesAccess.ReadMultipleStories(listOfStoriesID);

        //    // use mapper to return what its needed.
        //    return StoriesRepositoryMapper.MapToStoriesResponse(result, projectAcronym);
        //}

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.ReadStory">
        /// </summary>
        public async Task<StoryResponse> ReadStory(string storyId)
        {
            return await _storiesAccess.ReadStory(storyId);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.RemoveStory">
        /// </summary>
        public async Task<bool> RemoveStory(string storyId)
        {
            return await _storiesAccess.RemoveStory(storyId);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.StartStory">
        /// </summary>
        public async Task<StoryResponse> StartStory(StoryCreationRequest storyRequest)
        {
            return await _storiesAccess.StartStory(storyRequest);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccessProxy.UpdateStory">
        /// </summary>
        public async Task<StoryResponse> UpdateStory(string storyId, StoryUpdateRequest storyRequest)
        {
            return await _storiesAccess.UpdateStory(storyId, storyRequest);
        }
    }
}
