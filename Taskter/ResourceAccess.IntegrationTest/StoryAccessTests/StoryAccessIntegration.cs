using Microsoft.Extensions.DependencyInjection;
using StoriesAccessComponent;
using System;
using Xunit;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public class StoryAccessIntegration : IClassFixture<StoriesResourceFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStoriesAccess _storiesAccess;
        //private readonly IProjectCreationBuilder _projectBuilder;
        //private readonly IProjectUpdateBuilder _updateBuilder;
        private readonly StoriesResourceFixture _fixture;

        public StoryAccessIntegration(StoriesResourceFixture fixture)
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components : Only works because of the Microsoft.Extensions.DependencyInjection
            _storiesAccess = _serviceProvider.GetService<IStoriesAccess>();

            //// Test components
            //_projectBuilder = _serviceProvider.GetService<IProjectCreationBuilder>();
            //_updateBuilder = _serviceProvider.GetService<IProjectUpdateBuilder>();
        }
        //TODO: StartStory
        //TODO: ReadStoriesForAProject
        //TODO: ReadStory
        //TODO: UpdateStory
        //TODO: RemoveStory

    }
}
