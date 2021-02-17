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
        Task<ProjectResponse> GetSingleProject(string projectAcronym);

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        Task<IEnumerable<ProjectResponse>> GetProjects(string projectAcronym);

        /// <summary>
        /// Creates a project.
        /// </summary>
        Task<ProjectResponse> CreateProject(ProjectCreationRequest projectRequest);

        // TODO: Delete project
        // TODO: update project metadata


    }
}
