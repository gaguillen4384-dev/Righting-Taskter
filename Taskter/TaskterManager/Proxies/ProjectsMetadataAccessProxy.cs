using ProjectsMetadataAccessComponent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy">
    /// </summary>
    public class ProjectsMetadataAccessProxy : IProjectsMetadataAccessProxy
    {
        private IProjectsMetadataAccess _projectsMetadataAccess;

        public ProjectsMetadataAccessProxy(IProjectsMetadataAccess projectConnection)
        {

            _projectsMetadataAccess = projectConnection;

        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.CreateProjectMetadataDetails">
        /// </summary>
        public async Task<ProjectMetadataDetails> CreateProjectMetadataDetails(string projectAcronym)
        {
            return await _projectsMetadataAccess.CreateProjectMetadataDetails(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.GetAllProjectsMetadataDetails">
        /// </summary>
        public async Task<IEnumerable<ProjectMetadataDetails>> GetAllProjectsMetadataDetails()
        {
            return await _projectsMetadataAccess.GetAllProjectsMetadataDetails();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.GetLatestStoryNumberForProject">
        /// </summary>
        public async Task<int> GetLatestStoryNumberForProject(string projectAcronym)
        {
            return await _projectsMetadataAccess.GetLatestStoryNumberForProject(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.GetProjectMetadataDetails">
        /// </summary>
        public async Task<ProjectMetadataDetails> GetProjectMetadataDetails(string projectAcronym)
        {
            return await _projectsMetadataAccess.GetProjectMetadataDetails(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.RemoveProjectMetadataDetails">
        /// </summary>
        public async Task RemoveProjectMetadataDetails(string projectAcronym)
        {
            await _projectsMetadataAccess.RemoveProjectMetadataDetails(projectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.UpdateProjectMetadataAcronym">
        /// </summary>
        public async Task<ProjectMetadataDetails> UpdateProjectMetadataAcronym(string projectAcronym, string updatedProjectAcronym)
        {
            return await _projectsMetadataAccess.UpdateProjectMetadataAcronym(projectAcronym, updatedProjectAcronym);
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccessProxy.UpdateProjectMetadataDetails">
        /// </summary>
        public async Task UpdateProjectMetadataDetails(string projectAcronym, bool isCompleted = false)
        {
            await _projectsMetadataAccess.UpdateProjectMetadataDetails(projectAcronym, isCompleted);
        }
    }
}
