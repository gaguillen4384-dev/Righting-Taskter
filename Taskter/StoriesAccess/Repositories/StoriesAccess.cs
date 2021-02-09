using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace StoriesAccess.Repositories
{
    /// <summary>
    /// Concrete implementation of <see cref="IStoriesAccess"/>
    /// </summary>
    public class StoriesAccess : IStoriesAccess
    {
        //TODO: Implement with Mongo
        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.CreateStory(string, StoryRequest)">
        /// </summary>
        public Task<StoryResponse> CreateStory(string projectAcronym, StoryRequest storyRequest)
        {
            // this should join with a joint table where the table has the latest number for the story
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.GetProjectStories(string)">
        /// </summary>
        public Task<IEnumerable<StoryResponse>> GetProjectStories(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Concrete implementation of <see cref="IStoriesAccess.GetSingleStory(string, string)">
        /// </summary>
        public Task<StoryResponse> GetSingleStory(string projectAcronym, string storyNumber)
        {
            using (var connection = new SqliteConnection("Data Source=Stories.db"))
            {
                connection.Open();


                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT Story
                    FROM Stories
                    WHERE Project = $projectAcronym
                    AND StoryNumber = $storyNumber
                    ";
                command.Parameters.AddWithValue("$projectAcronym", projectAcronym);
                command.Parameters.AddWithValue("$storyNumber", storyNumber);
                command.ExecuteNonQuery();
                using (var reader = command.ExecuteReader())
                {
                    // TODO the PcCount comes from a seperate db ( a joint table K-V of storyid and pccount)
                    while (reader.Read())
                    {
                        // TODO: integrate the mapper
                       // send it to mapper.
                       // return a storyresponse
                    }
                }
            }

            return null;
        }
    }
}
