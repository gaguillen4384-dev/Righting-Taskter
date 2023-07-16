using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectsAccessComponent
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectAccess"/>
    /// </summary>
    // GETTO: if users become a thing then this needs change.
    public class ProjectsAccess : IProjectAccess
    {
        private ProjectsResource _projectConnection;

        public ProjectsAccess(IOptions<ProjectsResource> projectConnection) 
        {
            // This needs to be full path to open .db file
            _projectConnection = projectConnection.Value;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.StartProject(ProjectCreationRequest)"/>
        /// </summary>
        public async Task<ProjectResponse> StartProject(ProjectCreationRequest projectRequest)
        {
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                // Unlike stories, project own their own reference.
                projectsCollection.EnsureIndex(project => project.ProjectAcronym);

                var projectDocument = ProjectRepositoryMapper.MapToProjectDocumentFromCreationRequest(projectRequest);

                // GETTO: what to do if it fails? -upto the API that recieves it.
                projectsCollection.Insert(projectDocument);

                // GETTO: move to Manager.
                //var projectDetailsDocument = await CreateProjectMetadataDetails(projectDocument.ProjectAcronym);
                //await CreateStoryReferenceForProject(projectDocument.ProjectAcronym, projectDocument.Id.ToString());
                //var projectDetails = ProjectRepositoryMapper.MapToProjectNumbersDetails(projectDetailsDocument);

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(projectDocument);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.OpenProjects()"/>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> OpenProjects()
        {
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var projects = projectsCollection.Find(Query.All());

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectsResponse(projects);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.OpenProject(string)"/>
        /// </summary>
        public async Task<ProjectResponse> OpenProject(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(project);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.RemoveProject(string)"/>
        /// </summary>
        public async Task<bool> RemoveProject(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                //var project = projectsCollection.FindOne(projectToFind => projectToFind.ProjectAcronym == projectAcronym);
                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                if (project == null)
                    return false;

                return projectsCollection.Delete(project.Id);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.UpdateProject(ProjectUpdateRequest, string)"/>
        /// </summary>
        public async Task<ProjectResponse> UpdateProject(ProjectUpdateRequest projectRequest, string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                // with the story, map the new updated fields
                var projectUpdated = ProjectRepositoryMapper.MapToProjectDocumentFromUpdateRequest(project, projectRequest);

                var updated = projectsCollection.Update(project);

                // return a null object if failed to update.
                if (!updated)
                    return ProjectRepositoryMapper.MapToEmptyProjectResponse();

                // GETTO: Move to Managers.
                //ProjectMetadataDetails projectDetails = new EmptyProjectNumbersDetails();
                //// GETTO: If acronym is the same no changes.
                //if (ProjectRepositoryMapper.IsProjectAcronymUpdated(projectRequest, projectAcronym)) 
                //{
                //    projectDetails = await UpdateProjectAcronymReference(projectRequest.ProjectAcronym, projectAcronym, project.Id.ToString());
                //    // return a null object if failed to update.
                //    if (projectDetails is EmptyProjectNumbersDetails)
                //        return ProjectRepositoryMapper.MapToEmptyProjectResponse();
                //}

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(projectUpdated);
            }
        }

        #region Private Methods



        #endregion
    }
}
