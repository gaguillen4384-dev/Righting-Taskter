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
        public Task<ProjectResponse> CreateProject(ProjectCreationRequest projectRequest)
        {
            // create a storynumber as part of the project creation.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.GetProjects(string)"/>
        /// </summary>
        public Task<IEnumerable<ProjectResponse>> GetProjects(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectAccess.GetSingleProject(string)"/>
        /// </summary>
        public Task<ProjectResponse> GetSingleProject(string projectAcronym)
        {
            throw new System.NotImplementedException();
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
                    LatestStoryNumber = 1
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
        private async Task CreateProjectReference(string projectAcronym, ObjectId projectId) 
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\StoryReference.db"))
            {
                // this creates or gets collection
                var storiesReferenceCollection = db.GetCollection<StoryReferenceDocument>("StoryReferences");

                storiesReferenceCollection.EnsureIndex(reference => reference.ProjectAcronym);

                var storyReference = new StoryReferenceDocument()
                {
                    ProjectAcronym = projectAcronym,
                    ProjectId = projectId
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
