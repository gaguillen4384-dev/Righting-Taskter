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
        Task<StoryResponse> ReadStory(string storyId);

        /// <summary>
        /// Retrieves a list of stories, from a list Id.
        /// </summary>
        Task<IEnumerable<StoryResponse>> ReadMultipleStories(IEnumerable<string> storiesID);

        //TODO: move to Manager.
        ///// <summary>
        ///// Retrieves all stories for the given project.
        ///// </summary>
        //Task<IEnumerable<StoryResponse>> ReadStoriesForAProject(string projectAcronym);

        /// <summary>
        /// Creates a story for the given project.
        /// </summary>
        Task<StoryResponse> StartStory(string projectAcronym, StoryCreationRequest storyRequest);

        /// <summary>
        /// Updates a specific story for the given project.
        /// </summary>
        Task<StoryResponse> UpdateStory(string projectAcronym, string storyId, StoryUpdateRequest storyRequest);

        /// <summary>
        /// Deletes a specific story for the given project.
        /// </summary>
        Task<bool> RemoveStory(string projectAcronym, string storyId);

    }
}
