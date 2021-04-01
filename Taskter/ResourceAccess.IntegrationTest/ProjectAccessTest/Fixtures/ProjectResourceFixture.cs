using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectAccessComponent;
using System;
using System.Configuration;
using System.IO;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectResourceFixture : IDisposable
    {
        // TODO: Figure out the IOptions and where the sys grabbing it from.
        
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public ProjectResourceFixture()
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
            services.Configure<ProjectResource>(options => _configuration
                .GetSection(nameof(ProjectResource))
                .Bind(options));

            services.Configure<ProjectNumbersResource>(options => _configuration
                .GetSection(nameof(ProjectNumbersResource))
                .Bind(options));

            services.Configure<StoryReferenceResource>(options => _configuration
                .GetSection(nameof(StoryReferenceResource))
                .Bind(options));


            services.AddTransient<IProjectAccess, ProjectAccess>();

            // TEST SERVICES
            services.AddTransient<IProjectCreationBuilder, ProjectBuilder>();
            services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }


        public void PopulateProjectCollection(int numberOfProjects) 
        {
            var projectResource = ServiceProvider.GetService<IOptions<ProjectResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it
            using (var db = new LiteDatabase(projectResource.Value.ConnectionString))
            {
                var projectsCollection = db.GetCollection<ProjectDocument>("Projects");

                // Ensureindex might need to be called after object creation
                projectsCollection.EnsureIndex(project => project.ProjectAcronym);

                ProjectBuilder projectBuilder = new ProjectBuilder();
                var listOfProjectRequest = projectBuilder.BuildManyProjects(numberOfProjects);

                foreach (var projectRequest in listOfProjectRequest)
                {
                    // here make a detail for each project
                    PopulateProjectDetail(projectRequest.ProjectAcronym);

                    var projectDocument = ProjectRepositoryMapper.MapToProjectDocumentFromCreationRequest(projectRequest);
                    projectsCollection.Insert(projectDocument);
                }

                var projects = projectsCollection.Find(Query.All());
            }
        }

        // TODO: PRIVATE Create a Builder for ProjectNumbers
        private void PopulateProjectDetail(string projectAcronym)
        {
            var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectNumbersResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it
            using (var db = new LiteDatabase(projectDetailsResource.Value.ConnectionString))
            {
                // this creates or gets collection
                var projectNumberCollection = db.GetCollection<ProjectsStoryNumberDocument>("ProjectsStoryNumbers");

                // Index Document on name property
                projectNumberCollection.EnsureIndex(projectNum => projectNum.ProjectAcronym);

                ProjectNumbersDetailsBuilder projectDetailsBuilder = new ProjectNumbersDetailsBuilder();
                var projectNumber = projectDetailsBuilder.BuildProjectDetailsWithAcronym(projectAcronym);

                projectNumberCollection.Insert(projectNumber.Build());
            }
        }


        public void Dispose()
        {
            var projectResource = ServiceProvider.GetService<IOptions<ProjectResource>>();
            var projectDetailsResource = ServiceProvider.GetService<IOptions<ProjectNumbersResource>>();
            var storyReferenceResource = ServiceProvider.GetService<IOptions<StoryReferenceResource>>();
            // delete DB from file system.
            File.Delete(projectResource.Value.ConnectionString);
            File.Delete(projectDetailsResource.Value.ConnectionString);
            File.Delete(storyReferenceResource.Value.ConnectionString);
        }
    }
}
