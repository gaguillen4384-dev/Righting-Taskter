using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Responsible for interfacing with the ProjectAccess subsystem.
    /// </summary>
    public interface IProjectsAccessProxy
    {
        /// <summary>
        /// Retrieves a single project based on project acronym.
        /// </summary>
        Task<ProjectResponse> GetSingleProject(string projectAcronym);

        /// <summary>
        /// To 
        /// </summary>
        Task<ProjectResponse> UpdateSingleProject(ProjectRequest projectRequest);

        /// <summary>
        /// Based on a project acronym a single project will return. 
        /// </summary>
        Task<bool> DeletionSingleProject(string projectAcronym);

    }
}
