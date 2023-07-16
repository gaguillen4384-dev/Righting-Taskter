using StoriesReferencesAccessComponent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager
{
    public class StoriesReferencesAccessProxy : IStoriesReferencesAccessProxy
    {
        private IStoriesReferencesAccess _storiesReferenceAccess;

        public StoriesReferencesAccessProxy(IStoriesReferencesAccess projectConnection)
        {
            _storiesReferenceAccess = projectConnection;
        }

        public async Task<string> GetProjectId(string projectAcronym)
        {
            return await _storiesReferenceAccess.GetProjectId(projectAcronym);
        }

        public async Task<IEnumerable<string>> GetProjectStoriesIds(string projectAcronym)
        {
            return await _storiesReferenceAccess.GetProjectStoriesIds(projectAcronym);
        }

        public async Task<string> GetSingleStoryId(string projectAcronym, int storyNumber)
        {
            return await _storiesReferenceAccess.GetSingleStoryId(projectAcronym, storyNumber);
        }

        public async Task MakeReferenceForStoryInProject(string projectAcronym, int storyNumber, string storyId, string projectId)
        {
            await _storiesReferenceAccess.MakeReferenceForStoryInProject(projectAcronym, storyNumber, storyId, projectId);
        }

        public async Task<bool> RemoveReferenceOfStory(string storyId)
        {
            return await _storiesReferenceAccess.RemoveReferenceOfStory(storyId);
        }

        public async Task StartStoriesReferenceForProject(string projectAcronym, string projectId)
        {
            await _storiesReferenceAccess.StartStoriesReferenceForProject(projectAcronym, projectId);
        }

        public async Task UpdateStoryReferenceAcronym(string updateProjectAcronym, string projectId)
        {
            await _storiesReferenceAccess.UpdateStoryReferenceAcronym(updateProjectAcronym, projectId);
        }
    }
}
