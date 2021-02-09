using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectsAccessProxy">
    /// </summary>
    public class ProjectsAccessProxy : IProjectsAccessProxy
    {
        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.DeletionSingleProject(string)">
        /// </summary>
        public Task<bool> DeletionSingleProject(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.GetSingleProject(string)">
        /// </summary>
        public Task<ProjectResponse> GetSingleProject(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.UpdateSingleProject(ProjectRequest)">
        /// </summary>
        public Task<ProjectResponse> UpdateSingleProject(ProjectRequest projectRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
