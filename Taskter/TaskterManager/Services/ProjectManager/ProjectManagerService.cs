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
            //GETTO: things should instantiate with its own id.
            //GETTO: If these doesnt return the ID then its an issue because then I got to call into repo for it.
            var newProjectId = await _projectAccessProxy.StartProject(project); 

            var projectDetailsDocument = await _projectsMetadataAccessProxy.CreateProjectMetadataDetails(project.ProjectAcronym);
            await _storiesReferencesAccessProxy.StartStoriesReferenceForProject(project.ProjectAcronym, newProjectId);
            //var projectDetails = ProjectRepositoryMapper.MapToProjectNumbersDetails(projectDetailsDocument); GETTO: figure out what this is.

            return newProjectId;
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
            var result = await CombineProjectsAndMetadata(projectList, projectsMetadataList);
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

            return await CombineProjectsAndMetadata(projectList, projectMetadataList);
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

        #region Mrivate methods

        //GETTO: See if I need a mapper services just to help this thing. Might be a static boy.
        private async Task<IEnumerable<ProjectResponse>> CombineProjectsAndMetadata(List<ProjectResponse> projectResponses, List<ProjectMetadataDetails> projectMetadataList)
        {
            var result = new List<ProjectResponse>();
            foreach (var projectResponse in projectResponses)
            {
                var localMetadata = projectMetadataList.FirstOrDefault(x => x.ProjectAcronym == projectResponse.ProjectAcronym);
                if (localMetadata is null)
                    continue;

                projectResponse.LatestStoryNumber = localMetadata.LatestStoryNumber;
                projectResponse.DateCreated = localMetadata.DateCreated;
                projectResponse.DateUpdated = localMetadata.DateUpdated;
                projectResponse.NumberOfActiveStories = localMetadata.NumberOfActiveStories;
                projectResponse.NumberOfCompletedStories = localMetadata.NumberOfStoriesCompleted;
                projectResponse.LastWorkedOn = localMetadata.LastWorkedOn;
                result.Add(projectResponse);
            }

            return result;
        }

        //GETTO: move to Manger.
        //private async Task<ProjectMetadataDetails> UpdateProjectAcronymReference(string updatedProjectAcronym, string projectAcronym, string projectId) 
        //{
        //    await UpdateStoryReferenceAcronym(updatedProjectAcronym, projectId);
        //    return await UpdateProjectMetadataAcronym(projectAcronym, updatedProjectAcronym);
        //}
        #endregion
    }
}
