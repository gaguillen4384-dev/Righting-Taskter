using LiteDB;
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
    public class ProjectAccess : IProjectAccess
    {
        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.CreateProject(string, ProjectCreationRequest)"/>
        /// </summary>
        public async Task<ProjectResponse> StartProject(ProjectCreationRequest projectRequest)
        {
            using (var db = new LiteDatabase(@"\Projects.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                var projectDocument = ProjectRepositoryMapper.MapToProjectDocumentFromRequest(projectRequest);

                // TODO: what to do if it fails?
                projectsCollection.Insert(projectDocument);
                await CreateProjectStoryNumber(projectDocument.ProjectAcronym);
                await CreateProjectReference(projectDocument.ProjectAcronym, projectDocument._id.ToString());

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(projectDocument);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.OpenProjects(string)"/>
        /// </summary>
        public async Task<IEnumerable<ProjectResponse>> OpenProjects()
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\Projects.db"))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                // TODO: if users become a thing then this needs change.
                var result = projectsCollection.Find(Query.All());

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectsResponse(result);
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

                var result = projectsCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                // use mapper to return what its needed.
                return ProjectRepositoryMapper.MapToProjectResponse(result);
            }
        }

        #region Private Methods


        #endregion

        #region StoryNumber Access

        /// <summary>
        /// This creates K-V that stores the last number of the project.
        /// </summary>
        private async Task CreateProjectStoryNumber(string projectAcronym)
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
                    ProjectAcronym = projectAcronym,
                    LatestStoryNumber = 0
                };

                projectNumberCollection.Insert(projectNumber);
            }
        }

        #endregion

        #region StoryReferenceAccess

        /// <summary>
        /// Create a project reference.
        /// </summary>
        // TODO: Should I pass the objectId as string and parse it to the objectID
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
        private async Task UpdateProjectStoriesReference(string updateProjectAcronym, ObjectId projectId)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                var projectStories = storiesReferenceCollection.Find(Query.EQ("ProjectId", projectId));

                // TODO: send it to a mapper
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
