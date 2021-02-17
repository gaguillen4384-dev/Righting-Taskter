using LiteDB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Document;

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


        #region StoryNumber Access

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

        #endregion
    }
}
