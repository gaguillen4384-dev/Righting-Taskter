using System;
using System.Collections.Generic;
using Utilities.Domain;

namespace StoriesAccessComponent
{
    /// <summary>
    /// Responsible for mapping Stories to stories reponses.
    /// </summary>
    // TODO: this needs to be a automapper thing.
    public static class StoriesRepositoryMapper
    {
        /// <summary>
        /// Returns a Story Response from a resourceAccess story.
        /// </summary>
        public static StoryResponse MapToStoryResponse(Story story) 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a Stories Response from a resourceAccess stories.
        /// </summary>
        public static IEnumerable<StoryResponse> MapToStoriesResponse(IEnumerable<Story> stories)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a Story object from Story Creation Request.
        /// </summary>
        public static Story MapToStory(StoryCreationRequest storyRequest) 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a Story object from Story Update Request.
        /// </summary>
        public static Story MapToStory(StoryUpdateRequest storyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
