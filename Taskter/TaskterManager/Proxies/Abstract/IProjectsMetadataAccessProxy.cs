using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    public interface IProjectsMetadataAccessProxy
    {
        /// <summary>
        /// Creates a refence that stores the project metadata, returns the actual DB object.
        /// </summary>
        Task<ProjectMetadataDetails> CreateProjectMetadataDetails(string projectAcronym);

        /// <summary>
        /// Retrieves project metadata details for a given project.
        /// </summary>
        Task<ProjectMetadataDetails> GetProjectMetadataDetails(string projectAcronym);

        /// <summary>
        /// Retrieves all the projects metadata details.
        /// </summary>
        Task<IEnumerable<ProjectMetadataDetails>> GetAllProjectsMetadataDetails();

        /// <summary>
        /// Updates the project metadata details.
        /// </summary>
        Task UpdateProjectMetadataDetails(string projectAcronym, bool isCompleted = false);

        /// <summary>
        /// Updates the project metadata identifier.
        /// </summary>
        Task<ProjectMetadataDetails> UpdateProjectMetadataAcronym(string projectAcronym, string updatedProjectAcronym);

        /// <summary>
        /// Removes the project metadata.
        /// </summary>
        Task RemoveProjectMetadataDetails(string projectAcronym);

        /// <summary>
        /// Get the latest number on a project
        /// </summary>
        Task<int> GetLatestStoryNumberForProject(string projectAcronym);
    }
}
