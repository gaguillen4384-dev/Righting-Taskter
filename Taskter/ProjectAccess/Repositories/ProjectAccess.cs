using LiteDB;
using Utilities.Taskter.Domain;

namespace ProjectAccess.Repositories
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectAccess"/>
    /// </summary>
    public class ProjectAccess : IProjectAccess
    {
        /// <summary>
        /// This creates K-V that stores the last number of the project.
        /// </summary>
        private void CreateProjectStoryNumber(string projectAcronym)
        {
            /// TODO: the path got to be configure for each db.
            using (var db = new LiteDatabase(@"\ProjectsStoryNumber.db"))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumber");

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
    }
}
