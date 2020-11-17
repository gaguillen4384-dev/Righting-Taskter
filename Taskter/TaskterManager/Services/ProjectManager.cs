using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager
{
    /// <summary>
    /// Concrete implementation of <see cref="IProjectManager"/>
    /// </summary>
    public class ProjectManager : IProjectManager
    {
        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.CreateProject(ProjectDTO)"/>
        /// </summary>
        public Task<string> CreateProject(ProjectDTO project)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.EditProject(ProjectDTO)"/>
        /// </summary>
        public Task<string> EditProject(ProjectDTO project)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.GetProject(string)"/>
        /// </summary>
        public Task<ProjectDTO> GetProject(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Concrete implementation of <see cref="IProjectManager.GetProjects()"/>
        /// </summary>
        public Task<IList<ProjectDTO>> GetProjects()
        {
            throw new System.NotImplementedException();
        }
    }
}
