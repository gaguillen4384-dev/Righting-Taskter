using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Taskter.Domain;

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
        public static StoryResponse MapToStoryResponse(StoryDocument story,string projectAcronym) 
        {
            return new StoryResponse()
            {
               Name = story.Name,
               ProjectAcronymName = projectAcronym,
               StoryNumber = story.StoryNumber,
               Details = story.Details,
               DateCompleted = story.DateCompleted.GetValueOrDefault(),
               IsCompleted = story.IsCompleted,
               IsRecurrant = story.IsRecurrant,
               DateCreated = story.DateCreated,
               DateUpdated = story.DateUpdated
            };
        }

        /// <summary>
        /// Returns a Stories Response from a resourceAccess stories.
        /// </summary>
        public static IEnumerable<StoryResponse> MapToStoriesResponse(IEnumerable<StoryDocument> stories, string projectAcronym)
        {

            var storyResponses = new List<StoryResponse>();

            // Each story has a reference to its number but not projectAcronym
            foreach (var story in stories) 
            {
                storyResponses.Add(MapToStoryResponse(story, projectAcronym));
            }

            return storyResponses;
        }

        /// <summary>
        /// Returns a Story object from Story Creation Request.
        /// </summary>
        public static StoryDocument MapCreationRequestToStory(StoryCreationRequest storyRequest) 
        {
            return new StoryDocument()
            {
                Name = storyRequest.Name,
                Details = storyRequest.Details,
                IsRecurrant = storyRequest.IsRecurrant
            };
        }

        /// <summary>
        /// Returns given Story with updated properties from a Story Update Request.
        /// </summary>
        public static StoryDocument UpdateStoryPropertiesFromRequest(StoryDocument story, StoryUpdateRequest storyUpdate)
        {

            story.Name = IsStoryNameUpdated(storyUpdate) ? storyUpdate.Name : story.Name;
            story.Details = IsDetailsUpdated(storyUpdate) ? storyUpdate.Details : story.Details;
            story.IsRecurrant = IsStoryRecurrantStatusUpdated(storyUpdate) ? storyUpdate.IsRecurrant : story.IsRecurrant;

            // if is recurrant set then it cannot be completed
            if (!story.IsRecurrant)
                story.IsCompleted = IsStoryCompletedUpdated(storyUpdate) ? storyUpdate.IsCompleted : story.IsCompleted;

            return story;
        }

        /// <summary>
        /// Returns an empty Story Response.
        /// </summary>
        public static StoryResponse MapToEmptyStoryResponse()
        {
            return new EmptyStoryResponse();
        }

        private static bool IsStoryNameUpdated(StoryUpdateRequest storyUpdate)
        {
            return !string.IsNullOrWhiteSpace(storyUpdate.Name);
        }

        private static bool IsDetailsUpdated(StoryUpdateRequest storyUpdate)
        {
            var storyDetails = storyUpdate.Details;
            if (storyDetails == null)
                return false;

            if (!storyDetails.Any())
                return false;

            return true;
        }

        private static bool IsStoryCompletedUpdated(StoryUpdateRequest storyUpdate)
        {
            // if is recurrant set then it cannot be completed
            if (storyUpdate.IsCompleted)
                return true;

            return false;
        }

        private static bool IsStoryRecurrantStatusUpdated(StoryUpdateRequest storyUpdate)
        {
            if (storyUpdate.IsRecurrant)
                return true;

            return false;
        }
    }
}
