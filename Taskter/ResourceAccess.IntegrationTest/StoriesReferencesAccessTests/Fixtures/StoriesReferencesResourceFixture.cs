using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoriesReferencesAccessComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoriesReferencesAccessTests
{
    public class StoriesReferencesResourceFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IConfiguration _configuration;

        public StoriesReferencesResourceFixture()
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
            services.Configure<StoriesReferencesResource>(options => _configuration
                .GetSection(nameof(StoriesReferencesResource))
                .Bind(options));


            services.AddTransient<IStoriesReferencesAccess, StoriesReferencesAccess>();

            // TEST SERVICES
            services.AddTransient<IStoriesReferencesBuilder, StoriesReferencesBuilder>();
            //services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();
            //services.AddTransient<IProjectUpdateBuilder, ProjectUpdateBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }

        // Should it return an object with specif things?
        public FixtureResource PopulateStoriesCollection(int NumberOfStories, string? projectAcronym) 
        {
            var storiesResource = ServiceProvider.GetService<IOptions<StoriesReferencesResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it, bring a static service?
            using (var db = new LiteDatabase(storiesResource.Value.ConnectionString))
            {
                var storiesCollection = db.GetCollection<StoryReferenceDocument>("StoriesReferences");

                // Ensureindex might need to be called after object creation
                storiesCollection.EnsureIndex(story => story.Id);

                StoriesReferencesBuilder storiesReferenceBuilder = new StoriesReferencesBuilder();
                var listOfStories = storiesReferenceBuilder.BuildStoriesReferences(NumberOfStories).ToList();

                if (!string.IsNullOrWhiteSpace(projectAcronym)) 
                {
                    // Reset the setup
                    storiesReferenceBuilder = new StoriesReferencesBuilder();
                    listOfStories.Clear();
                    listOfStories = new List<StoryReferenceDocument>(storiesReferenceBuilder.BuildStoriesReferencesForSpecificProject(NumberOfStories, projectAcronym));
                }

                var result = new FixtureResource();

                foreach (var storyRequest in listOfStories)
                {
                    storiesCollection.Insert(storyRequest);
                    result.listOfProjectUsed.Add(storyRequest.ProjectAcronym);
                    result.listOfStoriesReferenceIds.Add(storyRequest.StoryId);
                    result.listOfProjectIds.Add(storyRequest.ProjectId);
                }

                return result;
            }
        }

        public void Dispose()
        {
            var storiesResource = ServiceProvider.GetService<IOptions<StoriesReferencesResource>>();
            // delete DB from file system.
            File.Delete(storiesResource.Value.ConnectionString);
        }
    }
}
