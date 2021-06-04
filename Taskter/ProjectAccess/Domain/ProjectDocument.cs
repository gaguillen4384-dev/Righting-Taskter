using LiteDB;
using LiteDbDriver;
using System;

namespace ProjectsAccessComponent
{
    public class ProjectDocument : BaseDocument
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Last date the project got worked on.
        /// </summary>
        public DateTime LastWorkedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The project acronym, used as an identifier.
        /// </summary>
        public string ProjectAcronym { get; set; } = string.Empty;
        
        [BsonCtor]
        public ProjectDocument() : base() { }

    }
}
