using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Responsible for encapsulating Project Management changes
    /// </summary>
    public interface IProjectManagerService
    {
        #region Project Management
      
        /// <summary>
        /// Creates a project.
        /// </summary>
        /// <returns>The project ID</returns>
        Task<string> CreateProject(ProjectCreationRequest project);

        /// <summary>
        /// Get All projects.
        /// </summary>
        Task<IEnumerable<ProjectResponse>> GetProjects();

        /// <summary>
        /// Get a project.
        /// </summary>
        Task<ProjectResponse> GetProject(string projectAcronym);

        /// <summary>
        /// Edit Project: atomic operations on the project stories and project own metadata.
        /// </summary>  
        /// <returns>The project ID</returns>
        Task<string> EditProject(ProjectUpdateRequest project);

        #endregion

        #region Project Management
        
        /// <summary>
        /// Create story
        /// </summary>
        Task CreateStory(string projectAcronym, StoryCreationRequest request);
        
        #endregion
    }
}
