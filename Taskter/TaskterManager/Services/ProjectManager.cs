using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Domain;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectManager"/>
    /// </summary>
    public class ProjectManager : IProjectManager
    {
        #region Project Management

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.CreateProject(ProjectRequest)"/>
        /// </summary>
        public Task<string> CreateProject(ProjectRequest project)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.GetProject(string)"/>
        /// </summary>
        Task<ProjectResponse> IProjectManager.GetProject(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.GetProjects()"/>
        /// </summary>
        Task<IList<ProjectResponse>> IProjectManager.GetProjects()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.EditProject(ProjectRequest)"/>
        /// </summary>
        public Task<string> EditProject(ProjectRequest project)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Stories Management

        /// <summary>
        /// 
        /// </summary>
        public Task CreateStory(string projectAcronym)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
