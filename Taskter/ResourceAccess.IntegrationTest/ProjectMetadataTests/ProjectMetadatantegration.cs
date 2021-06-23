using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoriesAccessComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public class ProjectMetadataFixtureIntegration : IClassFixture<ProjectMetadataFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStoriesAccess _storiesAccess;
        private readonly IStoriesBuilder _storiesBuilder;
        private readonly ProjectMetadataFixture _fixture;
        private readonly Random randomizer = new Random();

        public ProjectMetadataFixtureIntegration(ProjectMetadataFixture fixture)
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components : Only works because of the Microsoft.Extensions.DependencyInjection
            _storiesAccess = _serviceProvider.GetService<IStoriesAccess>();

            //// Test components
            _storiesBuilder = _serviceProvider.GetService<IStoriesBuilder>();
        }

        #region Start A story
        [Fact]
        public async void storyaccess_StartAStoryWithAParticularName_Success()
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

        #region Update stories
        [Fact]
        public async void StoriesAccess_UpdateAStory_Success()
        {
            // Arrange
            List<string> listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories).ToList();
            var idToUse = randomizer.Next(listOfIds.Count);

            var request = _storiesBuilder.UpdateStoryWithDetails(NaturalValues.NumberOfStoryDetails)
                                         .UpdateStoryWithIsCompleted(false)
                                         .UpdateStoryWithIsRecurrant(false)
                                         .UpdateStoryWithName(NaturalValues.UpdateStoryName)
                                         .BuildUpdateRequest();

            // Act
            var result = await _storiesAccess.UpdateStory(listOfIds[idToUse], request);

            // Assert - descriptive
            result.Should().BeOfType<StoryResponse>();
            result.As<StoryResponse>().Name.Should().Be(NaturalValues.UpdateStoryName);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: Create more update test with each property to update.

        #endregion

        #region Remove stories

        [Fact]
        public async void StoriesAccess_RemoveAStory_Success()
        {
            // Arrange
            List<string> listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories).ToList();
            var idToUse = randomizer.Next(listOfIds.Count);

            // Act
            var result = await _storiesAccess.RemoveStory(listOfIds[idToUse]);

            // Assert - descriptive
            result.Should().BeTrue();

            var resultAfterDelete = await _storiesAccess.ReadStory(listOfIds[idToUse]);
            resultAfterDelete.Should().BeOfType<EmptyStoryResponse>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        //TODO: Manager logic.
        ///// <summary>
        ///// Reads a story from a project.
        ///// </summary>
        //[Fact]
        //public async void StoryAccess_ReadSingleStoryFromWrongProject_EmptyResponse()
        //{
        //    // Arrange
        //    _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories);
        //    //var request = _storiesBuilder.BuildStoryWithName(NaturalValues.StoryName)
        //    //               .BuildStoryWithStoryNumber(NaturalValues.SingleStoryNumber)
        //    //               .BuildStoryWithDetails(NaturalValues.NumberOfStoryDetails)
        //    //               .Build();

        //    // Act
        //    var result = await _storiesAccess.ReadStory(NaturalValues.ProjectAcronymNoWorks, NaturalValues.SingleStoryNumber);

        //    // Assert - descriptive
        //    result.Should().BeOfType<EmptyProjectResponse>();

        //    // Teardown Needs to happen per test so other tests are not affected.
        //    _fixture.Dispose();
        //}


    }
}
