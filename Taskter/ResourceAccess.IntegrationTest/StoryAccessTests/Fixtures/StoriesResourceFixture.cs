using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoriesAccessComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


            services.AddTransient<IStoriesAccess, StoriesAccess>();

            // TEST SERVICES
            services.AddTransient<IStoriesBuilder, StoriesBuilder>();
            //services.AddTransient<IProjectNumbersBuilder, ProjectNumbersDetailsBuilder>();
            //services.AddTransient<IProjectUpdateBuilder, ProjectUpdateBuilder>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public IEnumerable<string> PopulateStoriesCollection(int NumberOfStories) 
        {
            var storiesResource = ServiceProvider.GetService<IOptions<StoriesResource>>();
            // TODO: Bring the inner logic to the litedbdriver and then reference it
            using (var db = new LiteDatabase(storiesResource.Value.ConnectionString))
            {
                var storiesCollection = db.GetCollection<StoryDocument>("Stories");

                // Ensureindex might need to be called after object creation
                storiesCollection.EnsureIndex(story => story.Id);

                var storiesBuilder = new StoriesBuilder();
                var listOfStoriesRequest = storiesBuilder.BuildStoriesOut(NumberOfStories);
                List<string> counter = new List<string>();
                foreach (var projectRequest in listOfStoriesRequest)
                {
                    var storyDocument = StoriesRepositoryMapper.MapCreationRequestToStory(projectRequest);
                    storiesCollection.Insert(storyDocument);
                    counter.Add(storyDocument.Id.ToString());
                }

                return counter;
            }
        }

        public void Dispose()
        {
            var storiesResource = ServiceProvider.GetService<IOptions<StoriesResource>>();
            // delete DB from file system.
            File.Delete(storiesResource.Value.ConnectionString);
        }
    }
}
