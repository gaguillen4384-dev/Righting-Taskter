using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectsMetadataAccessComponent
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectsMetadataAccess"/>
    /// </summary>
    public class ProjectsMetadataAccess : IProjectsMetadataAccess
    {
        private ProjectsMetadataResource _projectNumbersConnection;

        public ProjectsMetadataAccess(IOptions<ProjectsMetadataResource> projectsMetadataConnection)
        {
            // This needs to be full path to open .db file
            _projectNumbersConnection = projectsMetadataConnection.Value;
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.CreateProjectMetadataDetails"/>
        /// </summary>
        public async Task<ProjectMetadataDetails> CreateProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // Index Document on name property
                projectMetadataCollection.EnsureIndex(projectMetadataDetails => projectMetadataDetails.ProjectAcronym);

                var projectMetadata = new ProjectMetadataDocument()
                {
                    ProjectAcronym = projectAcronym
                };

                projectMetadataCollection.Insert(projectMetadata);

                return ProjectMetadataMapper.MapToProjectMetadataDetails(projectMetadata);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.GetProjectMetadataDetails"/>
        /// </summary>
        public async Task<ProjectMetadataDetails> GetProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // This needs to be generic in a driver.
                var projectMetadataDetails = projectMetadataCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                if (projectMetadataDetails == null)
                {
                    return ProjectMetadataMapper.MapToEmptyProjectMetadataDetails();
                }

                return ProjectMetadataMapper.MapToProjectMetadataDetails(projectMetadataDetails);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.GetAllProjectsMetadataDetails()"/>
        /// </summary>
        public async Task<IEnumerable<ProjectMetadataDetails>> GetAllProjectsMetadataDetails()
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                var result = projectMetadataCollection.Find(Query.All());

                return ProjectMetadataMapper.MapToProjectsMetadataDetails(result);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.GetLatestStoryNumberForProject"/>
        /// </summary>
        public async Task<int> GetLatestStoryNumberForProject(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // This needs to be generic in a driver.
                var result = projectNumberCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var projectNumber = result.FirstOrDefault();

                if (projectNumber == null)
                {
                    return 0;
                }

                return projectNumber.LatestStoryNumber;
            }
        }

        /// <summary>
        /// Updates ProjectMetadata
        /// </summary>
        public async Task UpdateProjectMetadataDetails(string projectAcronym, bool isCompleted = false)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // This needs to be generic in a driver.
                var result = projectNumberCollection.Find(Query.EQ("ProjectAcronym", projectAcronym));

                var projectNumber = result.FirstOrDefault();

                projectNumber.DateUpdated = DateTime.UtcNow;
                projectNumber.LatestStoryNumber++;
                projectNumber.NumberOfActiveStories++;

                if (isCompleted)
                {
                    projectNumber.NumberOfStoriesCompleted++;
                    projectNumber.NumberOfActiveStories--;
                }

                //GETTO: What if this fails?
                projectNumberCollection.Update(projectNumber);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.GetAllProjectsMetadataDetails"/>
        /// </summary>
        public async Task<ProjectMetadataDetails> UpdateProjectMetadataAcronym(string projectAcronym, string updatedProjectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                var projectMetadataDetails = projectMetadataCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                projectMetadataDetails.ProjectAcronym = updatedProjectAcronym;

                var updated = projectMetadataCollection.Update(projectMetadataDetails);
                if (updated == false)
                {
                    return ProjectMetadataMapper.MapToEmptyProjectMetadataDetails();
                }

                return ProjectMetadataMapper.MapToProjectMetadataDetails(projectMetadataDetails);
            }

        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.RemoveProjectMetadataDetails"/> 
        /// </summary>
        public async Task RemoveProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                var projectNumber = projectMetadataCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                projectMetadataCollection.Delete(projectNumber.Id);
            }
        }
    }
}
