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
        // GETTO: Validation is reponsability of this Layer.
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
        /// Concrete implementation of <see cref="IProjectManagerService.StartProject"/>
        /// </summary>
        public async Task<string> StartProject(ProjectCreationRequest project)
        {
            //GETTO: Same acronym is a no-no.
            //GETTO: Validate that the projectAcronym and name are present

            var result = await _projectAccessProxy.StartProject(project);

            //GETTO: start project reference and story reference. 

            return result.ProjectAcronym;

        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProject"/>
        /// </summary>
        public async Task<ProjectResponse> GetProject(string projectAcronym)
        {
            return await _projectAccessProxy.OpenProject(projectAcronym);
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProjects()"/>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> GetProjects()
        {
            return await _projectAccessProxy.OpenProjects();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.EditProject"/>
        /// </summary>
        public async Task<string> EditProject(ProjectUpdateRequest project)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Stories Management

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.CreateStory"/>
        /// </summary>
        public async Task CreateStory(string projectAcronym, StoryCreationRequest request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
