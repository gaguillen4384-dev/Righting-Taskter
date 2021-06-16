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

        #region Start A story
        [Fact]
        public async void StoriesReferencesAccess_MakeStoriesReferenceForProject_Success()
        {
            // Arrange
            _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories);
            var request = _storiesBuilder.BuildStoryWithName(NaturalValues.StoryName)
                           .BuildStoryWithStoryNumber(NaturalValues.SingleStoryNumber)
                           .BuildStoryWithDetails(NaturalValues.NumberOfStoryDetails)
                           .BuildCreateRequest();

            // Act
            var result = await _storiesAccess.StartStory(request);
            // Assert - descriptive
            result.Should().BeOfType<StoryResponse>();
            result.As<StoryResponse>().Name.Should().Be(NaturalValues.StoryName);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }
        #endregion

        #region Read stories
        /// <summary>
        /// Reads a story from a project. This is one is not specific, limitations of the system.
        /// </summary>
        [Fact]
        public async void StoriesAccess_ReadMultipleStories_Success()
        {
            // Arrange
            var listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories);

            // Act
            var result = await _storiesAccess.ReadMultipleStories(listOfIds);

            // Assert - descriptive
            result.Should().NotBeEmpty()
                .And.HaveCount(NaturalValues.NumberOfStories);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }
        /// <summary>
        /// Reads a story from a project. This is one is not specific, limitations of the system.
        /// </summary>
        [Fact]
        public async void StoriesAccess_ReadSingleStory_Success()
        {
            // Arrange
            List<string> listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories).ToList();

            var idToUse = randomizer.Next(listOfIds.Count);

            // Act
            var result = await _storiesAccess.ReadStory(listOfIds[idToUse]);

            // Assert - descriptive
            result.Should().BeOfType<StoryResponse>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion
    }
}
