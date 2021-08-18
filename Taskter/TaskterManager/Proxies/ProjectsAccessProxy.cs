using ProjectsAccessComponent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectsAccessProxy">
    /// </summary>
    public class ProjectsAccessProxy : IProjectsAccessProxy
    {
        private IProjectAccess _projectAccess;

        public ProjectsAccessProxy(IProjectAccess projectConnection)
        {
            _projectAccess = projectConnection;
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.OpenProject>
        /// </summary>
        public async Task<ProjectResponse> OpenProject(string projectAcronym)
        {
            return await _projectAccess.OpenProject(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.OpenProjects>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> OpenProjects()
        {
            return await _projectAccess.OpenProjects();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.RemoveProject>
        /// </summary>
        public async Task<bool> RemoveProject(string projectAcronym)
        {
            return await _projectAccess.RemoveProject(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.StartProject>
        /// </summary>
        public async Task<ProjectResponse> StartProject(ProjectCreationRequest projectRequest)
        {
            return await _projectAccess.StartProject(projectRequest);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsAccessProxy.UpdateProject>
        /// </summary>
        public async Task<ProjectResponse> UpdateProject(ProjectUpdateRequest projectRequest, string projectAcronym)
        {
            return await _projectAccess.UpdateProject(projectRequest, projectAcronym);
        }
    }
}
