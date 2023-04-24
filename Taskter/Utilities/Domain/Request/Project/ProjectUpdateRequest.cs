using System;

namespace Utilities.Taskter.Domain
{
    public class ProjectUpdateRequest
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The project acronym, used as an identifier.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;
    }
}
