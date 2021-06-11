using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoriesAccessComponent;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Random randomizer = new Random();

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


        //[fact]
        //public async void storyaccess_readmultiplestoryfromproject_success()
        //{
        //    // arrange
        //    _fixture.populateprojectcollection(naturalvalues.numberofprojectstobecreated);

        //    // act
        //    var result = await _projectaccess.openproject(naturalvalues.projectacronymtobeused);

        //    // assert - descriptive
        //    result.should().beoftype<projectresponse>();
        //    result.as<projectresponse>().projectacronym.should().be(naturalvalues.projectacronymtobeused);

        //    // teardown needs to happen per test so other tests are not affected.
        //    _fixture.dispose();
        //}

        /// <summary>
        /// Reads a story from a project. This is one is not specific, limitations of the system.
        /// </summary>
        [Fact]
        public async void StoryAccess_ReadMultipleStories_Success()
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
        public async void StoryAccess_ReadSingleStory_Success()
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

        //TODO: UpdateStory
        //TODO: RemoveStory

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
