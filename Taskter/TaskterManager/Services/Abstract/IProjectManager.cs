using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Responsible for encapsulating Project Management changes
    /// </summary>
    public interface IProjectManager
    {
        #region Project Management
      
        /// <summary>
        /// Creates a project.
        /// </summary>
        /// <returns>The project ID</returns>
        Task<string> CreateProject(ProjectRequest project);

        /// <summary>
        /// Get All projects.
        /// </summary>
        Task<IList<ProjectResponse>> GetProjects();

        /// <summary>
        /// Get a project.
        /// </summary>
        Task<ProjectResponse> GetProject(string projectAcronym);

        /// <summary>
        /// Edit Project: atomic operations on the project stories and project own metadata.
        /// </summary>  
        /// <returns>The project ID</returns>
        Task<string> EditProject(ProjectRequest project);

        #endregion

        #region Project Management
       
        //TODO: Interface with the engine here
        // StoryRequest needs to be a parameter
        Task CreateStory(string projectAcronym);
        
        #endregion
    }
}
