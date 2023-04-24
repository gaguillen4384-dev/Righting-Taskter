using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Responsible for interfacing with the ProjectAccess subsystem.
    /// </summary>
    public interface IProjectsAccessProxy
    {
        /// <summary>
        /// Retrieves a single project.
        /// </summary>
        Task<ProjectResponse> OpenProject(string projectAcronym);

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        Task<IEnumerable<ProjectResponse>> OpenProjects();

        /// <summary>
        /// Creates a project.
        /// </summary>
        Task<ProjectResponse> StartProject(ProjectCreationRequest projectRequest);

        /// <summary>
        /// Deletes a specific project and all the references.
        /// </summary>
        Task<bool> RemoveProject(string projectAcronym);

        /// <summary>
        /// Update a specific project and its references only if projectAcronym changed.
        /// </summary>
        Task<ProjectResponse> UpdateProject(ProjectUpdateRequest projectRequest, string projectAcronym);
    }
}
