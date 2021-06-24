using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectsMetadataAccessComponent;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public class ProjectMetadataFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public ProjectMetadataFixture()
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
            services.Configure<ProjectsMetadataResource>(options => _configuration
                .GetSection(nameof(ProjectsMetadataResource))
                .Bind(options));


            services.AddTransient<IProjectsMetadataAccess, ProjectsMetadataAccess>();

            // TEST SERVICES
            services.AddTransient<IProjectMetadataBuilder, ProjectMetadataBuilder>();
            //services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();
            //services.AddTransient<IProjectUpdateBuilder, ProjectUpdateBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public IEnumerable<string> PopulateProjectMetadataCollection(int NumberOfProjects) 
        {
            var storiesResource = ServiceProvider.GetService<IOptions<ProjectsMetadataResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it
            using (var db = new LiteDatabase(storiesResource.Value.ConnectionString))
            {
                var projectsMetadataCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

                // Ensureindex might need to be called after object creation
                projectsMetadataCollection.EnsureIndex(story => story.Id);

                ProjectMetadataBuilder projectMetadataBuilder = new ProjectMetadataBuilder();
                var listOfProjectRequest = projectMetadataBuilder.BuildManyProjectsOut(NumberOfProjects);
                List<string> counter = new List<string>();
                foreach (var projectRequest in listOfProjectRequest)
                {
                    projectsMetadataCollection.Insert(projectRequest);
                    counter.Add(projectRequest.Id.ToString());
                }

                return counter;
            }
        }

        public void Dispose()
        {
            var projectMetadataResource = ServiceProvider.GetService<IOptions<ProjectsMetadataResource>>();
            // delete DB from file system.
            File.Delete(projectMetadataResource.Value.ConnectionString);
        }
    }
}
