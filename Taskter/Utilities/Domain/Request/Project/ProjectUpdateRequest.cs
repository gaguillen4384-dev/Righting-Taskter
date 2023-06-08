using System;

namespace Utilities.Taskter.Domain
{
    public class ProjectUpdateRequest
    {
        /// <summary>
        /// The name of the project. Could be changed if desired.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The project acronym, used as new identifier.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;

        /// <summary>
        /// The existing project acronym, used as an identifier.
        /// </summary>
        public string ExistingProjectAcronym { get; set; } = string.Empty;

    }
}
