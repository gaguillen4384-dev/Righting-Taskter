using LiteDB;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

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
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.CreateProjectMetadataDetails(string)"/>
        /// </summary>
        // TODO: This could return the domain object instead of repo layer.
        public async Task<ProjectMetadataDetails> CreateProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // Index Document on name property
                projectNumberCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);

                var projectMetadata = new ProjectMetadataDocument()
                {
                    ProjectAcronym = projectAcronym
                };

                projectNumberCollection.Insert(projectMetadata);

                return ProjectRepositoryMapper.MapToProjectNumbersDetails(projectMetadata);
            }
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectsMetadataAccess.GetProjectMetadataDetails(string)"/>
        /// </summary>
        public async Task<ProjectMetadataDetails> GetProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // This needs to be generic in a driver.
                var projectNumber = projectNumberCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                if (projectNumber == null)
                {
                    return ProjectRepositoryMapper.MapToEmptyProjectNumbersDetails();
                }

                return ProjectRepositoryMapper.MapToProjectNumbersDetails(projectNumber);
            }
        }

        private async Task<IEnumerable<ProjectMetadataDetails>> GetAllProjectsMetadataDetails()
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectsCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // TODO: if users become a thing then this needs change.
                var result = projectsCollection.Find(Query.All());

                return ProjectRepositoryMapper.MapToProjectsNumbersDetails(result);
            }
        }

        private async Task<ProjectMetadataDetails> UpdateProjectMetadataAcronym(string projectAcronym, string updatedProjectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

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

        private async Task RemoveProjectMetadataDetails(string projectAcronym)
        {
            using (var db = new LiteDatabase(_projectNumbersConnection.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                var projectNumber = projectNumberCollection.FindOne(Query.EQ("ProjectAcronym", projectAcronym));

                projectNumberCollection.Delete(projectNumber.Id);
            }
        }
    }
}
