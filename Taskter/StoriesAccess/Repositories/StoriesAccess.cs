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
    public class StoriesAccess : IStoriesAccess
    {
        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.CreateStory(string, StoryCreationRequest)">
        /// </summary>
        public async Task<StoryResponse> CreateStory(string projectAcronym, StoryCreationRequest storyRequest)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Index Document on name property
                storiesCollection.EnsureIndex(storyx => storyx._id);

                // Map from request to story
                var story = StoriesRepositoryMapper.MapCreationRequestToStory(storyRequest);

                var latestStoryNumber = await GetLatestStoryNumberForProject(projectAcronym);
                var storyNumber = latestStoryNumber++;

                story.StoryNumber = storyNumber;
                storiesCollection.Insert(story);

                await UpdateStoryReferences(projectAcronym, storyNumber, story._id);

                return StoriesRepositoryMapper.MapToStoryResponse(story, projectAcronym);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.GetProjectStories(string)">
        /// </summary>
        public async Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym)
        {
            // use stories ID list, filter to find all stories
            var listOfStoriesID = await GetProjectStoriesIds(projectAcronym);
            var result = await GetProjectStoriesFromStoryIds(listOfStoriesID);

            // use mapper to return what its needed.
            return StoriesRepositoryMapper.MapToStoriesResponse(result, projectAcronym);

        }


        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.GetSingleStory(string, int)">
        /// </summary>
        public async Task<StoryResponse> GetSingleStory(string projectAcronym, int storyNumber)
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
        public Task<StoryResponse> UpdateStory(string projectAcronym, int storyNumber, StoryUpdateRequest storyRequest)
        {
            // TODO: Need an update function that automates the completion of a story and update information at the resouceaccess.

            throw new NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.DeleteStory(string, int)">
        /// </summary>
        public Task<StoryResponse> DeleteStory(string projectAcronym, int storyNumber)
        {
            // TODO: Need to delete from story db and storyrefence.
            throw new NotImplementedException();
        }

        #region Private Methods

        private async Task UpdateStoryReferences(string projectAcronym, int storyNumber, ObjectId storyId)
        {
            var projectId = await GetProjectId(projectAcronym);

            CreateReferenceForProjectAndStory(projectAcronym, storyNumber, storyId, projectId);

            UpdateLatestStoryNumberForProject(projectAcronym);
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
        private void UpdateLatestStoryNumberForProject(string projectAcronym)
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

                if (!projectNumberCollection.Update(projectNumber))
                {
                    throw new KeyNotFoundException("The project could not be found.");
                }
            }
        }

        #endregion

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
        // TODO: I dont want to be passing db specific types into method.
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
            }
        }

        #endregion
    }
}
