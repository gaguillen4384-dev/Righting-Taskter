using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public class StoriesResourceFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public void Dispose()
        {
            var projectResource = ServiceProvider.GetService<IOptions<StoriesResource>>();
            var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectNumbersResource>>();
            var storyReferenceResource = ServiceProvider.GetService<IOptions<StoryReferenceResource>>();
            // delete DB from file system.
            File.Delete(projectResource.Value.ConnectionString);
            File.Delete(projectDetailsResource.Value.ConnectionString);
            File.Delete(storyReferenceResource.Value.ConnectionString);
        }
    }
}
