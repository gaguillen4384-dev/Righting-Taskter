using System;

namespace Utilities.Taskter.Domain
{
    public class ProjectUpdateRequest
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// The project acronym, used as an identifier.
        /// </summary>
        public string ProjectAcronym = string.Empty;
    }
}
