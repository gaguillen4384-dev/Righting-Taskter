using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager
{
    public class StoriesReferencesAccessProxy : IStoriesReferencesAccessProxy
    {
        public Task<string> GetProjectId(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetProjectStoriesIds(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetSingleStoryId(string projectAcronym, int storyNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task MakeReferenceForStoryInProject(string projectAcronym, int storyNumber, string storyId, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveReferenceOfStory(string storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task StartStoriesReferenceForProject(string projectAcronym, string projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateStoryReferenceAcronym(string updateProjectAcronym, string projectId)
        {
            throw new System.NotImplementedException();
        }
    }
}
