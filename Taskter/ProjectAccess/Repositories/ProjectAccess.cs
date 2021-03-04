using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace ProjectAccessComponent
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectAccess"/>
    /// </summary>
    // TODO: if users become a thing then this needs change.
    public class ProjectAccess : IProjectAccess
    {
        private ProjectResourceConfig _projectConnection;

        public ProjectAccess(IOptions<ProjectResourceConfig> projectConnection) 
        {
            _projectConnection = projectConnection.Value;
        }
        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.StartProject(ProjectCreationRequest)"/>
        /// </summary>
        public async Task<ProjectResponse> StartProject(ProjectCreationRequest projectRequest)
        {
            // @"\Projects.db"
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                // Unlike stories, project own their own reference.
                projectsCollection.EnsureIndex(projectx => projectx.ProjectAcronym);

                var projectDocument = ProjectRepositoryMapper.MapToProjectDocumentFromCreationRequest(projectRequest);

                // TODO: what to do if it fails?
                projectsCollection.Insert(projectDocument);
                var projectDetailsDocument = await CreateProjectDetails(projectDocument.ProjectAcronym);
                await CreateProjectReference(projectDocument.ProjectAcronym, projectDocument._id.ToString());

                var projectDetails = ProjectRepositoryMapper.MapToProjectNumbersDetails(projectDetailsDocument);

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(projectDocument, projectDetails);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.OpenProjects()"/>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> OpenProjects()
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(_projectConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var projects = projectsCollection.Find(Query.All());

                var projectsDetails = await GetProjectsNumbersDetails();

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectsResponse(projects, projectsDetails);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.OpenProject(string)"/>
        /// </summary>
        public async Task<ProjectResponse> OpenProject(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Projects.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                // Get project numbers details
                var projectDetails = await GetProjectNumberDetails(projectAcronym);

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(project, projectDetails);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.RemoveStory(string)"/>
        /// </summary>
        public async Task<bool> RemoveStory(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Projects.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                return projectsCollection.Delete(project._id);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.UpdateProject(ProjectUpdateRequest, string)"/>
        /// </summary>
        public async Task<ProjectResponse> UpdateProject(ProjectUpdateRequest projectRequest, string projectAcronym)
        {
            // UPDATE reference, storynumbers, projectdetail if acronym changes
            using (var db = new LiteDatabase(@"\Projects.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var project = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                // with the story, map the new updated fields
                var projectUpdated = ProjectRepositoryMapper.MapToProjectDocumentFromUpdateRequest(project, projectRequest);

                var updated = projectsCollection.Update(project);

                ProjectNumbersDetails projectDetails = new EmptyProjectNumbersDetails();
                if (ProjectRepositoryMapper.IsProjectAcronymUpdated(projectRequest)) 
                {
                    projectDetails = await UpdateProjectAcronymReference(projectRequest.ProjectAcronym, projectAcronym, project._id.ToString());
                    // return a null object if failed to update.
                    if (projectDetails is EmptyProjectNumbersDetails)
                        return ProjectRepositoryMapper.MapToEmptyProjectResponse();
                }

                // return a null object if failed to update.
                if (!updated)
                    return ProjectRepositoryMapper.MapToEmptyProjectResponse();

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(project, projectDetails);
            }
        }

        #region Private Methods

        private async Task<ProjectNumbersDetails> UpdateProjectAcronymReference(string updatedProjectAcronym, string projectAcronym, string projectId) 
        {
            await UpdateProjectStoriesReferenceAcronym(updatedProjectAcronym, projectId);
            return await UpdateProjectNumbersAcronym(projectAcronym, updatedProjectAcronym);
        }

        #endregion

        #region StoryNumber Access

        /// <summary>
        /// This creates a refence that stores the last number of the project.
        /// </summary>
        private async Task<ProjectsStoryNumberDocument> CreateProjectDetails(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // Index Document on name property
                projectNumberCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);

                var projectNumber = new ProjectsStoryNumberDocument()
                {
                    ProjectAcronym = projectAcronym
                };

                projectNumberCollection.Insert(projectNumber);

                return projectNumber;
            }
        }

        private async Task<ProjectNumbersDetails> GetProjectNumberDetails(string projectAcronym) 
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // This needs to be generic in a driver.
                var projectNumber = projectNumberCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                if (projectNumber == null)
                {
                    return ProjectRepositoryMapper.MapToEmptyProjectNumbersDetails();
                }

                return ProjectRepositoryMapper.MapToProjectNumbersDetails(projectNumber);
            }
        }

        private async Task<IEnumerable<ProjectNumbersDetails>> GetProjectsNumbersDetails()
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // TODO: if users become a thing then this needs change.
                var result = projectsCollection.Find(Query.All());

                return ProjectRepositoryMapper.MapToProjectsNumbersDetails(result);
            }
        }

        private async Task<ProjectNumbersDetails> UpdateProjectNumbersAcronym(string projectAcronym, string updatedProjectAcronym) 
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                var projectNumber = projectNumberCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                projectNumber.ProjectAcronym = updatedProjectAcronym;

                var updated = projectNumberCollection.Update(projectNumber);
                if (updated == false)
                {
                    return ProjectRepositoryMapper.MapToEmptyProjectNumbersDetails();
                }

                return ProjectRepositoryMapper.MapToProjectNumbersDetails(projectNumber);
            }

        }

        #endregion

        #region StoryReferenceAccess

        /// <summary>
        /// Create a project reference.
        /// </summary>
        private async Task CreateProjectReference(string projectAcronym, string projectId) 
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                storiesReferenceCollection.EnsureIndex(reference => reference.ProjectAcronym);

                var projectDBId = new ObjectId(projectId);

                var storyReference = new StoryReferenceDocument()
                {
                    ProjectAcronym = projectAcronym,
                    ProjectId = projectDBId
                };

                storiesReferenceCollection.Insert(storyReference);

                // TODO: What to do if insert fails?
            }
        }

        /// <summary>
        /// Updates a project reference.
        /// </summary>
        private async Task UpdateProjectStoriesReferenceAcronym(string updateProjectAcronym, string projectId)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                // TODO: might need to pass in a objectId
                var projectStories = storiesReferenceCollection.Find(Query.EQ("ProjectId", projectId));

                foreach (var storyReference in projectStories) 
                {
                    storyReference.DateUpdated = DateTime.UtcNow;
                    storyReference.ProjectAcronym = updateProjectAcronym;
                }

                storiesReferenceCollection.Update(projectStories);
            }
        }
        #endregion
    }
}
