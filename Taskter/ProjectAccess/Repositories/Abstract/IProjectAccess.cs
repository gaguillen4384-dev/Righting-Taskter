using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectAccessComponent
{ 
    public interface IProjectAccess
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

        // TODO: Delete project, should it delete projectreference and all subsequent story references?
        // TODO: update project metadata


    }
}
