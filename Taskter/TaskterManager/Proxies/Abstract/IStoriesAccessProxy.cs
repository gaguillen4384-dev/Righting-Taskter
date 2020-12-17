using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    public interface IStoriesAccessProxy
    {
        /// <summary>
        /// Retrieves a single project based on project acronym.
        /// </summary>
        Task<ProjectResponse> GetSingleStory(string projectAcronym, string storyIdentifier);

        /// <summary>
        /// To 
        /// </summary>
        Task<ProjectResponse> GetProjectStories(ProjectRequest projectRequest);

    }
}
