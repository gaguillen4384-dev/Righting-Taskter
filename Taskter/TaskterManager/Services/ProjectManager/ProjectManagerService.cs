using System;
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
            //GETTO: should have validation and appropriate response. a Validator is needed.
            var newProject = await _projectAccessProxy.StartProject(project); 

            await _projectsMetadataAccessProxy.CreateProjectMetadataDetails(project.ProjectAcronym);
            await _storiesReferencesAccessProxy.StartStoriesReferenceForProject(project.ProjectAcronym, newProject.Id);

            return newProject.ProjectAcronym;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.GetProject"/>
        /// </summary>
        public async Task<ProjectResponse> GetProject(string projectAcronym)
        {
            //GETTO: This should return metadata with the project.
            var project = await _projectAccessProxy.OpenProject(projectAcronym);
            var projectList = new List<ProjectResponse>
            {
                project
            }; //GETTO: Could make this into a static function so it can be reused.

            var projectsMetadata = await _projectsMetadataAccessProxy.GetProjectMetadataDetails(projectAcronym);
            var projectsMetadataList = new List<ProjectMetadataDetails>
            {
                projectsMetadata
            };
            var result = await ManagerMapper.CombineProjectsAndMetadata(projectList, projectsMetadataList);
            return result.FirstOrDefault();
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

            return await ManagerMapper.CombineProjectsAndMetadata(projectList, projectMetadataList);
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectManagerService.EditProject"/>
        /// </summary>
        public async Task<string> EditProject(ProjectUpdateRequest project)
        {
            // Before I was expecting the RA layer to do the check if needs to update or not, because I was using local resource 
            // Now Since I don't have local resource I get to leverage the Client, It got to know existing and new to use
            //GETTO: Figure out how to test the update of project and metadata. if projectacronym changes.
            //GETTO: Update the project given, it could be 1 or the other or both. but exisiting 

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

        #region Mrivate methods

        #endregion
    }
}
