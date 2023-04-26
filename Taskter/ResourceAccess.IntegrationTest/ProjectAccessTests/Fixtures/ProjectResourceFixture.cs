using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectsAccessComponent;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTests
{
    public class ProjectResourceFixture : IDisposable
    {     
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public ProjectResourceFixture()
        {
            // Initialize stuff
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(NaturalValues.AppsettingsLocation, false)
                .Build();
            _configuration = configuration;

            // this is by Microsoft.Extensions.Options.ConfigurationExtensions & ConfigurationBinder which allows strongtype 
            // Supposed to fill in IOptions
            services.Configure<ProjectsResource>(options => _configuration
                .GetSection(nameof(ProjectsResource))
                .Bind(options));

            services.AddTransient<IProjectAccess, ProjectsAccess>();

            // TEST SERVICES
            services.AddTransient<IProjectCreationBuilder, ProjectBuilder>();
            //services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();
            services.AddTransient<IProjectUpdateBuilder, ProjectUpdateBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }


        public IEnumerable<string> PopulateProjectCollection(int numberOfProjects) 
        {
            var projectResource = ServiceProvider.GetService<IOptions<ProjectsResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it
            using (var db = new LiteDatabase(projectResource.Value.ConnectionString))
            {
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                // Ensureindex might need to be called after object creation
                projectsCollection.EnsureIndex(project => project.ProjectAcronym);

                ProjectBuilder projectBuilder = new ProjectBuilder();
                var listOfProjectRequest = projectBuilder.BuildManyProjects(numberOfProjects);
                List<string> counter = new List<string>();
                foreach (var projectRequest in listOfProjectRequest)
                {
                    var projectDocument = ProjectRepositoryMapper.MapToProjectDocumentFromCreationRequest(projectRequest);
                    projectsCollection.Insert(projectDocument);
                    counter.Add(projectDocument.Id.ToString());
                }

                return counter;
            }
        }

        //GETTO: move to ProjectMetadataRA
        //// TODO: PRIVATE Create a Builder for ProjectNumbers
        //private void PopulateProjectDetail(string projectAcronym)
        //{
        //    var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectsMetadataResource>>();
        //    // TODO: Bring the inner logic to the litedbdriver and then reference it
        //    using (var db = new LiteDatabase(projectDetailsResource.Value.ConnectionString))
        //    {
        //        // this creates or gets collection
        //        var projectNumberCollection = db.GetCollection<ProjectMetadataDocument>("ProjectsMetadata");

        //        // Index Document on name property
        //        projectNumberCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);

        //        ProjectNumbersDetailsBuilder projectDetailsBuilder = new ProjectNumbersDetailsBuilder();
        //        var projectNumber = projectDetailsBuilder.BuildProjectDetailsWithAcronym(projectAcronym);

        //        projectNumberCollection.Insert(projectNumber.Build());
        //    }
        //}


        public void Dispose()
        {
            var projectResource = ServiceProvider.GetService<IOptions<ProjectsResource>>();
            //var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectsMetadataResource>>();
            //var storyReferenceResource = ServiceProvider.GetService<IOptions<StoriesReferencesResource>>();
            // delete DB from file system.
            File.Delete(projectResource.Value.ConnectionString);
            //File.Delete(projectDetailsResource.Value.ConnectionString);
            //File.Delete(storyReferenceResource.Value.ConnectionString);
        }
    }
}
