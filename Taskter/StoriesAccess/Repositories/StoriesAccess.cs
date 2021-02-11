using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Domain;

namespace StoriesAccessComponent.Repositories
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
                var storiesCollection = db.GetCollection<Story>("Stories");

                // Map from request to story
                var story = StoriesRepositoryMapper.MapToStory(storyRequest);

                var latestStoryNumber = GetLatestStoryNumberForProject(projectAcronym);

                story.StoryNumber = latestStoryNumber++;

                storiesCollection.Insert(story);

                // Index Document on name property
                storiesCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);

                return StoriesRepositoryMapper.MapToStoryResponse(story);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.GetProjectStories(string)">
        /// </summary>
        public async Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Stories.db"))
            {
                // this creates or gets collection
                var storiesCollection = db.GetCollection<Story>("Stories");

                // This needs to be generic in a driver.
                var result = storiesCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                // use mapper to return what its needed.
                return StoriesRepositoryMapper.MapToStoriesResponse(result);
            }
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
                var storiesCollection = db.GetCollection<Story>("Stories");

                // This needs to be generic in a driver.
                var result = storiesCollection.Find(Query.And(
                    Query.EQ("ProjectAcronym", projectAcronym),
                    Query.EQ("StoryNumber", storyNumber)));

                var listResult = result.ToList();

                // use mapper to return what its needed.
                return StoriesRepositoryMapper.MapToStoryResponse(listResult.FirstOrDefault());
            }
        }

        #region NumbersAccess for story access.
        /// <summary>
        /// This retrieves a K-V that stores the last number of the project.
        /// </summary>
        private int GetLatestStoryNumberForProject(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumber>("ProjectsStoryNumber");

                // This needs to be generic in a driver.
                var result = projectNumberCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var projectNumber = result.FirstOrDefault();

                if (projectNumber == null) 
                {
                    return 0;
                }

                // use mapper to return what its needed.
                return projectNumber.LatestStoryNumber;
            }
        }

        /// <summary>
        /// This creates K-V that stores the last number of the project.
        /// </summary>
        // TODO: THis belongs at the ProjectAccess 
        private void CreateProjectStoryNumber(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumber>("ProjectsStoryNumber");

                var projectNumber = new ProjectsStoryNumber()
                {
                    ProjectAcronym = projectAcronym,
                    LatestStoryNumber = 1
                };

                projectNumberCollection.Insert(projectNumber);

                // Index Document on name property
                projectNumberCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);
            }
        }

        #endregion
    }
}
