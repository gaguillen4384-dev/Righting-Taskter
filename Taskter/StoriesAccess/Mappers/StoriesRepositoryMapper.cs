using System;
using System.Collections.Generic;
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
        /// Returns a Story object from Story Update Request.
        /// </summary>
        public static StoryDocument MapUpdateRequestToStory(StoryUpdateRequest storyRequest)
        {
            throw new NotImplementedException();
        }
    }
}
