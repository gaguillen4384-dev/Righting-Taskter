using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace StoriesReferencesAccessComponent
{
    public class StoriesReferencesAccess
    {
        private StoriesReferencesResource _storyReferenceResource;

        public StoriesReferencesAccess(IOptions<StoriesReferencesResource> storyReferenceResource)
        {
            // This needs to be full path to open .db file
            _storyReferenceResource = storyReferenceResource.Value;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesReferencesAccess.CreateStoryReferenceForProject(string, string)"/>
        /// </summary>
        public async Task CreateStoryReferenceForProject(string projectAcronym, string projectId)
        {
            using (var db = new LiteDatabase(_storyReferenceResource.ConnectionString))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoriesReferences");

                storiesReferenceCollection.EnsureIndex(reference => reference.ProjectAcronym);

                var projectDBId = new ObjectId(projectId);

                var storyReference = new StoryReferenceDocument()
                {
                    ProjectAcronym = projectAcronym,
                    ProjectId = projectDBId
                };

                storiesReferenceCollection.Insert(storyReference);

                // TODO: What to do if insert fails?
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IStoriesReferencesAccess.UpdateStoryReferenceAcronym(string, string)"/>
        /// </summary>
        public async Task UpdateStoryReferenceAcronym(string updateProjectAcronym, string projectId)
        {
            using (var db = new LiteDatabase(_storyReferenceResource.ConnectionString))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoriesReferences");

                var projectStories = storiesReferenceCollection.Find(Query.EQ("ProjectId", projectId));

                foreach (var storyReference in projectStories)
                {
                    storyReference.DateUpdated = DateTime.UtcNow;
                    storyReference.ProjectAcronym = updateProjectAcronym;
                }

                storiesReferenceCollection.Update(projectStories);
            }
        }
    }
}
