using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace StoriesAccessComponent
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesAccess"/>
    /// </summary>
    public class StoriesAccess : IStoriesAccess
    {
        private StoriesResource _storiesConnection;
        private ProjectsMetadataResource _projectNumbersConnection;
        private StoriesReferencesResource _storyReferenceResource;

        public StoriesAccess(IOptions<StoriesResource> storiesConnection,
          IOptions<ProjectsMetadataResource> projectNumbersConnection,
          IOptions<StoriesReferencesResource> storyReferenceResource)
        {
            // This needs to be full path to open .db file
            _storiesConnection = storiesConnection.Value;
            _projectNumbersConnection = projectNumbersConnection.Value;
            _storyReferenceResource = storyReferenceResource.Value;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.StartStory(string, StoryCreationRequest)">
        /// </summary>
        public async Task<StoryResponse> StartStory(string projectAcronym, StoryCreationRequest storyRequest)
        {
            using (var db = new LiteDatabase(_storiesConnection.ConnectionString))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Index Document on name property
                storiesCollection.EnsureIndex(storyx => storyx.Id);

                // Map from request to story
                var story = StoriesRepositoryMapper.MapCreationRequestToStory(storyRequest);

                //TODO: Copy to Manager.
                //var latestStoryNumber = await GetLatestStoryNumberForProject(projectAcronym);
                //var storyNumber = latestStoryNumber++;

                storiesCollection.Insert(story);

                //TODO: Copy to Manager.
                //await UpdateStoryReferences(projectAcronym, storyNumber, story.Id);

                return StoriesRepositoryMapper.MapToStoryResponse(story, projectAcronym);
            }
        }

        //TODO: move to Manager.
        ///// <summary>
        ///// Concrete implementation of <see cref="IStoriesAccess.ReadStoriesForAProject(string)">
        ///// </summary>
        //public async Task<IEnumerable<StoryResponse>> ReadStoriesForAProject(string projectAcronym)
        //{
        //    // use stories ID list, filter to find all stories
        //    var listOfStoriesID = await GetProjectStoriesIds(projectAcronym);
        //    var result = await GetProjectStoriesFromStoryIds(listOfStoriesID);

        //    // use mapper to return what its needed.
        //    return StoriesRepositoryMapper.MapToStoriesResponse(result, projectAcronym);

        //}

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.ReadStory(string)">
        /// </summary>
        public async Task<StoryResponse> ReadStory(string storyId)
        {
            using (var db = new LiteDatabase(_storiesConnection.ConnectionString))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                var story = storiesCollection.FindOne();

                if (story == null) 
                {
                    return new EmptyStoryResponse();
                }

                // use mapper to return what its needed.
                return StoriesRepositoryMapper.MapToStoryResponse(story, projectAcronym);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.UpdateStory(string, int, StoryUpdateRequest)">
        /// </summary>
        public async Task<StoryResponse> UpdateStory(string projectAcronym, int storyNumber, StoryUpdateRequest storyRequest)
        {
            using (var db = new LiteDatabase(_storiesConnection.ConnectionString))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                var story = storiesCollection.FindOne(Query.EQ("StoryNumber", storyNumber));

                // with the story, map the new updated fields
                var storyUpdated = StoriesRepositoryMapper.UpdateStoryPropertiesFromRequest(story, storyRequest);

                var updated = storiesCollection.Update(storyUpdated);

                //TODO: move to ProjectMetadataRA
                //if (storyUpdated.IsCompleted)
                //    UpdateStoryNumberForProject(projectAcronym, storyUpdated.IsCompleted);

                // return a null object if failed to update.
                if (!updated)
                    return StoriesRepositoryMapper.MapToEmptyStoryResponse();

                // use mapper to return what its needed.
                return StoriesRepositoryMapper.MapToStoryResponse(storyUpdated, projectAcronym);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.RemoveStory(string, int)">
        /// </summary>
        public async Task<bool> RemoveStory(string projectAcronym, int storyNumber)
        {
            using (var db = new LiteDatabase(_storiesConnection.ConnectionString))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                var story = storiesCollection.FindOne(Query.EQ("StoryNumber", storyNumber));

                if (!storiesCollection.Delete(story.StoryNumber))
                    return false;

                //TODO: this needs to be moved to manager
                //if (!DeleteReferenceForStory(storyId))
                //    return false;

                return true;
            }
        }

        #region Private Methods

        //TODO: Copy to Manager
        //private async Task UpdateStoryReferences(string projectAcronym, int storyNumber, ObjectId storyId)
        //{
        //    var projectId = await GetProjectId(projectAcronym);

        //    CreateReferenceForProjectAndStory(projectAcronym, storyNumber, storyId, projectId);

        //    UpdateStoryNumberForProject(projectAcronym);
        //}

        private async Task<IEnumerable<StoryDocument>> GetProjectStoriesFromStoryIds(IEnumerable<string> storiesID)
        {
            var listResult = new List<StoryDocument>();
            using (var db = new LiteDatabase(_storiesConnection.ConnectionString))
            {
                foreach (var storyId in storiesID) 
                {
                    // this creates or gets collection
                    var storiesCollection = db.GetCollection<StoryDocument>("Stories");
                    var result = storiesCollection.FindById(storyId);
                    listResult.Add(result);
                }
            }
            return listResult;
        }


        #endregion

    }
}
