using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoriesAccessComponent;
using StoriesReferencesAccessComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.StoriesReferencesAccessTests
{
    public class StoriesReferencesAccessIntegration : IClassFixture<StoriesReferencesResourceFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly StoriesReferencesResourceFixture _fixture;
        private readonly IStoriesReferencesAccess _storiesReferencesAccess;
        private readonly IStoriesReferencesBuilder _storiesReferencesBuilder;
        private readonly Random randomizer = new Random();

        public StoriesReferencesAccessIntegration(StoriesReferencesResourceFixture fixture)
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components : Only works because of the Microsoft.Extensions.DependencyInjection
            _storiesReferencesAccess = _serviceProvider.GetService<IStoriesReferencesAccess>();

            //// Test components
            _storiesReferencesBuilder = _serviceProvider.GetService<IStoriesReferencesBuilder>();
        }

        //TODO: TESTS with different project names, wrong number for project, empty projects
        //TODO: Update Reference acronym
        //TODO: RemoveReference
        //TODO: GetProjectId , not found

        /// <summary>
        /// Reads a story from a project. This is one is not specific, limitations of the system.
        /// </summary>
        [Fact]
        public async void StoriesAccess_ReadMultipleStories_Success()
        {
            // Arrange
            var listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories).ToList();

            var nameToUse = randomizer.Next(listOfIds.Count);

            // Act
            var result = await _storiesReferencesAccess.GetProjectId(listOfIds[nameToUse]);

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: GetProjectStoriesIds
        //TODO: GetSingleStoryId
        //TODO: MakeReferenceForProjectAndStory
        //TODO: StartStoriesReferenceForProject

    }
}
