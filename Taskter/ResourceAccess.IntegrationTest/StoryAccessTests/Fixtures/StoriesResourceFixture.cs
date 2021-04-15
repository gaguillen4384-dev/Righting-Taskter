using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoriesAccessComponent;
using System;
using System.IO;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public class StoriesResourceFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public StoriesResourceFixture()
        {
            // Initialize stuff
            var services = new ServiceCollection();

            // TODO: programtically get the appsettingslocation by directory and file classes.
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(NaturalValues.AppsettingsLocation, false)
                .Build();
            _configuration = configuration;

            // this is by Microsoft.Extensions.Options.ConfigurationExtensions & ConfigurationBinder which allows strongtype 
            // Supposed to fill in IOptions
            services.Configure<StoriesResource>(options => _configuration
                .GetSection(nameof(StoriesResource))
                .Bind(options));

            services.Configure<ProjectNumbersResource>(options => _configuration
                .GetSection(nameof(ProjectNumbersResource))
                .Bind(options));

            services.Configure<StoryReferenceResource>(options => _configuration
                .GetSection(nameof(StoryReferenceResource))
                .Bind(options));


            services.AddTransient<IStoriesAccess, StoriesAccess>();

            // TEST SERVICES
            //services.AddTransient<IProjectCreationBuilder, ProjectBuilder>();
            //services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();
            //services.AddTransient<IProjectUpdateBuilder, ProjectUpdateBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }

            public void Dispose()
        {
            var storiesResource = ServiceProvider.GetService<IOptions<StoriesResource>>();
            var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectNumbersResource>>();
            var storyReferenceResource = ServiceProvider.GetService<IOptions<StoryReferenceResource>>();
            // delete DB from file system.
            File.Delete(storiesResource.Value.ConnectionString);
            File.Delete(projectDetailsResource.Value.ConnectionString);
            File.Delete(storyReferenceResource.Value.ConnectionString);
        }
    }
}
