using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager
{
    /// <summary>
    /// Responsible for encapsulating Project Management changes
    /// </summary>
    public interface IProjectManager
    {
        /// <summary>
        /// Creates a project.
        /// </summary>
        /// <returns>The project ID</returns>
        Task<string> CreateProject(ProjectDTO project);

        /// <summary>
        /// Get ALL a project.
        /// </summary>
        Task<IList<ProjectDTO>> GetProjects();

        /// <summary>
        /// Get a project.
        /// </summary>
        Task<ProjectDTO> GetProject(string id);

        /// <summary>
        /// Edit Project: atomic operations on the project stories and project own metadata.
        /// </summary>  
        /// <returns>The project ID</returns>
        Task<string> EditProject(ProjectDTO project);
    }
}
