using LiteDbDriver;
using System;

namespace ProjectAccessComponent
{
    public class ProjectDocument : BaseDocument
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// Last date the project got worked on.
        /// </summary>
        public DateTime LastWorkedOn = DateTime.UtcNow;

        /// <summary>
        /// The project acronym, used as an identifier.
        /// </summary>
        public string ProjectAcronym = string.Empty;
    }
}
