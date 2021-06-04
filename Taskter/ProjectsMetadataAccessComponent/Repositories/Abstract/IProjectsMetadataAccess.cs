using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace ProjectsMetadataAccessComponent
{
    /// <summary>
    /// Responsible for a project metadata.
    /// </summary>
    public interface IProjectsMetadataAccess
    {
        /// <summary>
        /// Creates a refence that stores the project metadata, returns the actual DB object.
        /// </summary>
        Task<ProjectMetadataDetails> CreateProjectMetadataDetails(string projectAcronym);

        /// <summary>
        /// Retrieves project metadata details for 
        /// </summary>
        Task<ProjectMetadataDetails> GetProjectMetadataDetails(string projectAcronym)
    }
}
