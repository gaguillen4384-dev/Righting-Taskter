using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace StoriesAccessComponent
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesAccess"/>
    /// </summary>
    // TODO: if users become a thing then this needs change.
    public class StoriesAccess : IStoriesAccess
    {
        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.StartStory(string, StoryCreationRequest)">
        /// </summary>
        public async Task<StoryResponse> StartStory(string projectAcronym, StoryCreationRequest storyRequest)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Index Document on name property
                storiesCollection.EnsureIndex(storyx => storyx.Id);

                // Map from request to story
                var story = StoriesRepositoryMapper.MapCreationRequestToStory(storyRequest);

                var latestStoryNumber = await GetLatestStoryNumberForProject(projectAcronym);
                var storyNumber = latestStoryNumber++;

                story.StoryNumber = storyNumber;
                storiesCollection.Insert(story);

                await UpdateStoryReferences(projectAcronym, storyNumber, story.Id);

                return StoriesRepositoryMapper.MapToStoryResponse(story, projectAcronym);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.ReadStoriesForAProject(string)">
        /// </summary>
        public async Task<IEnumerable<StoryResponse>> ReadStoriesForAProject(string projectAcronym)
        {
            // use stories ID list, filter to find all stories
            var listOfStoriesID = await GetProjectStoriesIds(projectAcronym);
            var result = await GetProjectStoriesFromStoryIds(listOfStoriesID);

            // use mapper to return what its needed.
            return StoriesRepositoryMapper.MapToStoriesResponse(result, projectAcronym);

        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.ReadStory(string, int)">
        /// </summary>
        public async Task<StoryResponse> ReadStory(string projectAcronym, int storyNumber)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Use story Reference to get ID
                var storyId = await GetSingleStoryId(projectAcronym, storyNumber);

                var result = storiesCollection.FindById(storyId);

                // use mapper to return what its needed.
                return StoriesRepositoryMapper.MapToStoryResponse(result, projectAcronym);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.UpdateStory(string, int, StoryUpdateRequest)">
        /// </summary>
        public async Task<StoryResponse> UpdateStory(string projectAcronym, int storyNumber, StoryUpdateRequest storyRequest)
        {
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Use story Reference to get ID
                var storyId = await GetSingleStoryId(projectAcronym, storyNumber);

                var story = storiesCollection.FindById(storyId);

                // with the story, map the new updated fields
                var storyUpdated = StoriesRepositoryMapper.UpdateStoryPropertiesFromRequest(story, storyRequest);

                var updated = storiesCollection.Update(storyUpdated);

                if (storyUpdated.IsCompleted)
                    UpdateStoryNumberForProject(projectAcronym, storyUpdated.IsCompleted);

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
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Use story Reference to get ID
                var storyId = await GetSingleStoryId(projectAcronym, storyNumber);

                if (!storiesCollection.Delete(storyId))
                    return false;

                if (!DeleteReferenceForStory(storyId))
                    return false;

                return true;
            }
        }

        #region Private Methods

        private async Task UpdateStoryReferences(string projectAcronym, int storyNumber, ObjectId storyId)
        {
            var projectId = await GetProjectId(projectAcronym);

            CreateReferenceForProjectAndStory(projectAcronym, storyNumber, storyId, projectId);

            UpdateStoryNumberForProject(projectAcronym);
        }

        private async Task<IEnumerable<StoryDocument>> GetProjectStoriesFromStoryIds(IEnumerable<string> storiesID)
        {
            var listResult = new List<StoryDocument>();
            using (var db = new LiteDatabase(@"\Stories.db"))
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

        //TODO: Make this its own access
        #region NumbersAccess

        /// <summary>
        /// This retrieves a K-V that stores the last number of the project.
        /// </summary>
        private async Task<int> GetLatestStoryNumberForProject(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // This needs to be generic in a driver.
                var result = projectNumberCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var projectNumber = result.FirstOrDefault();

                if (projectNumber == null) 
                {
                    return 0;
                }

                return projectNumber.LatestStoryNumber;
            }
        }

        /// <summary>
        /// This retrieves a K-V that stores the last number of the project.
        /// </summary>
        private void UpdateStoryNumberForProject(string projectAcronym, bool isCompleted = false)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // This needs to be generic in a driver.
                var result = projectNumberCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var projectNumber = result.FirstOrDefault();

                projectNumber.DateUpdated = DateTime.UtcNow;
                projectNumber.LatestStoryNumber++;
                projectNumber.NumberOfActiveStories++;

                if (isCompleted) 
                {
                    projectNumber.NumberOfStoriesCompleted++;
                    projectNumber.NumberOfActiveStories--;
                }

                if (!projectNumberCollection.Update(projectNumber))
                {
                    throw new KeyNotFoundException("The project could not be found.");
                }
            }
        }

        #endregion

        //TODO: Make this its own access
        #region StoryReferenceAccess

        /// <summary>
        /// Gets the story id from StoryReferences.
        /// </summary>
        private async Task<string> GetSingleStoryId(string projectAcronym, int storyNumber)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                // This needs to be generic in a driver.
                var result = storiesReferenceCollection.FindOne(Query.And(
                    Query.EQ("ProjectAcronym", projectAcronym),
                    Query.EQ("StoryNumber", storyNumber)));
                
                return result.StoryId.ToString();
            }
        }

        /// <summary>
        /// Gets the story ids from StoryReferences.
        /// </summary>
        private async Task<IEnumerable<string>> GetProjectStoriesIds(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                // This needs to be generic in a driver.
                var result = storiesReferenceCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var listResult = result.ToList();

                var listIdsResult = listResult.Select(reference => reference.StoryId.ToString());

                return listIdsResult;
            }
        }

        /// <summary>
        /// Gets the project id from StoryReferences.
        /// </summary>
        private async Task<ObjectId> GetProjectId(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                // This needs to be generic in a driver.
                var result = storiesReferenceCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                return result.ProjectId;
            }
        }

        /// <summary>
        /// Creates the story Reference
        /// </summary>
        // TODO: I dont want to be passing db specific types into method. I could pass in a string a map it to objectID
        private void CreateReferenceForProjectAndStory(string projectAcronym, int storyNumber, ObjectId storyId, ObjectId projectId)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                storiesReferenceCollection.EnsureIndex(reference => reference.ProjectAcronym);
                
                var storyReference = new StoryReferenceDocument()
                {
                    ProjectAcronym = projectAcronym,
                    StoryNumber = storyNumber,
                    StoryId = storyId,
                    ProjectId = projectId
                };

                storiesReferenceCollection.Insert(storyReference);

                // TODO: What to do if insert fails?
            }
        }

        /// <summary>
        /// Creates the story Reference
        /// </summary>
        private bool DeleteReferenceForStory(string storyId)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                var storyObjectID = GetSingleStoryReferenceId(storyId).Result;

                if (!storiesReferenceCollection.Delete(storyObjectID))
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Get the story reference ID, ONLY USE FOR STORYREFERENCE ACCESS.
        /// </summary>
        private async Task<string> GetSingleStoryReferenceId(string storyId)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                // This needs to be generic in a driver.
                var result = storiesReferenceCollection.FindOne(Query.EQ("StoryId", storyId));

                return result.Id.ToString();
            }
        }

        #endregion
    }
}
