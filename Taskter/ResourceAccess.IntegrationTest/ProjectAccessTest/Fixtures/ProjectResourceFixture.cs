using Microsoft.Extensions.Options;
using System;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectResourceFixture<TStartup> : IDisposable where TStartup : class
    {
        // TODO: Figure out the IOptions and where the sys grabbing it from.
        public ProjectResourceFixture(IOptions<ProjectResourceConfig> projectResourceConfig)
        {
            // Initialize stuff
        }

        public void Dispose()
        {
            // Dispose of stuff
        }
    }
}
