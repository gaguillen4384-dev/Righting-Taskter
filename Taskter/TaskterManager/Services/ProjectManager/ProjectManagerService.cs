using System.Collections.Generic;
using System.Linq;
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

        //Managers should split up in action oriented manner.
        #region Project Management

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.CreateProject"/>
        /// </summary>
        public async Task<string> CreateProject(ProjectCreationRequest project)
        {
            //GETTO: should have validation and appropriate response.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProject"/>
        /// </summary>
        public async Task<ProjectResponse> GetProject(string projectAcronym)
        {
            //GETTO: This should return metadata with the project.
            //GETTO:          After getting project get metadata.
            return await _projectAccessProxy.OpenProject(projectAcronym);
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProjects()"/>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> GetProjects()
        {
            var projects = await _projectAccessProxy.OpenProjects();
            var projectList = projects.ToList();

            var projectsMetadata = await _projectsMetadataAccessProxy.GetAllProjectsMetadataDetails();
            var projectMetadataList = projectsMetadata.ToList();

            //GETTO: get all the metadata combines with the projects. 
            //GETTO: See if I need a combiner services just to help this thing. Might be a static boy.

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
