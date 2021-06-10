using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoriesAccessComponent;
using System;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public class StoryAccessIntegration : IClassFixture<StoriesResourceFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStoriesAccess _storiesAccess;
        private readonly IStoriesBuilder _storiesBuilder;
        private readonly StoriesResourceFixture _fixture;

        public StoryAccessIntegration(StoriesResourceFixture fixture)
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components : Only works because of the Microsoft.Extensions.DependencyInjection
            _storiesAccess = _serviceProvider.GetService<IStoriesAccess>();

            //// Test components
            _storiesBuilder = _serviceProvider.GetService<IStoriesBuilder>();
        }

        //TODO: StartStory


        //[Fact]
        //public async void StoryAccess_ReadSingleStoryFromProject_Success()
        //{
        //    // Arrange
        //    _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);

        //    // Act
        //    var result = await _projectAccess.OpenProject(NaturalValues.ProjectAcronymToBeUsed);

        //    // Assert - descriptive
        //    result.Should().BeOfType<ProjectResponse>();
        //    result.As<ProjectResponse>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToBeUsed);

        //    // Teardown Needs to happen per test so other tests are not affected.
        //    _fixture.Dispose();
        //}

        /// <summary>
        /// Reads a story from a project.
        /// </summary>
        [Fact]
        public async void StoryAccess_ReadSingleStoryFromProject_Success()
        {
            // Arrange
            _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories);

            //TODO: Create builder.
            //var request = _storiesBuilder.BuildStoryWithName(NaturalValues.StoryName + NaturalValues.SingleStoryNumber)
            //               .BuildStoryWithProjectAcronym(NaturalValues.ProjectAcronymWorks+NaturalValues.SingleStoryNumber)
            //               .BuildStoryWithStoryNumber(NaturalValues.SingleStoryNumber)
            //               .BuildStoryWithDetails(NaturalValues.NumberOfStoryDetails)
            //               .Build();

            // Act
            var result = await _storiesAccess.ReadStory(NaturalValues.SingleStoryNumber);

            // Assert - descriptive
            result.Should().BeOfType<StoryResponse>();
            result.As<StoryResponse>().ProjectAcronymName.Should().Be(NaturalValues.ProjectAcronymWorks);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        /// <summary>
        /// Reads a story from a project.
        /// </summary>
        [Fact]
        public async void StoryAccess_ReadSingleStoryFromWrongProject_EmptyResponse()
        {
            // Arrange
            _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStories);
            var request = _storiesBuilder.BuildStoryWithName(NaturalValues.StoryName)
                           .BuildStoryWithProjectAcronym(NaturalValues.ProjectAcronymWorks)
                           .BuildStoryWithStoryNumber(NaturalValues.SingleStoryNumber)
                           .BuildStoryWithDetails(NaturalValues.NumberOfStoryDetails)
                           .Build();

            // Act
            var result = await _storiesAccess.ReadStory(NaturalValues.ProjectAcronymNoWorks, NaturalValues.SingleStoryNumber);

            // Assert - descriptive
            result.Should().BeOfType<EmptyProjectResponse>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }
        //TODO: UpdateStory
        //TODO: RemoveStory

    }
}
