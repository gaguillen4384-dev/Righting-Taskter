using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectManagerService"/>
    /// </summary>
    public class ProjectManagerService : IProjectManagerService
    {
        // TODO: Validation is reponsability of this Layer.
        private readonly IProjectsAccessProxy _projectAccessProxy;
        private readonly IStoriesAccessProxy _storiesAccessProxy;
        private readonly IStoriesReferencesAccessProxy _storiesReferencesAccessProxy;
        private readonly IProjectsMetadataAccessProxy _projectsMetadataAccessProxy;

        public ProjectManagerService(
            IProjectsAccessProxy projectsAccessProxy,
            IStoriesAccessProxy storiesAccessProxy,
            IStoriesReferencesAccessProxy storiesReferencesAccessProxy,
            IProjectsMetadataAccessProxy projectsMetadataAccessProxy) 
        {
            _projectAccessProxy = projectsAccessProxy;
            _storiesAccessProxy = storiesAccessProxy;
            _storiesReferencesAccessProxy = storiesReferencesAccessProxy;
            _projectsMetadataAccessProxy = projectsMetadataAccessProxy;
        }

        #region Project Management

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.CreateProject"/>
        /// </summary>
        public Task<string> CreateProject(ProjectCreationRequest project)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProject"/>
        /// </summary>
        Task<ProjectResponse> IProjectManagerService.GetProject(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProjects()"/>
        /// </summary>
        Task<IList<ProjectResponse>> IProjectManagerService.GetProjects()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.EditProject"/>
        /// </summary>
        public Task<string> EditProject(ProjectUpdateRequest project)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Stories Management

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.CreateStory"/>
        /// </summary>
        public Task CreateStory(string projectAcronym, StoryCreationRequest request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
