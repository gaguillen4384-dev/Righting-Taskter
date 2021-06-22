using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StoriesReferencesAccessComponent;
using System;
using System.Linq;
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

        #region Get project ID

        [Fact]
        public async void StoriesReferences_GetProjectById_Success()
        {
            // Arrange
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, null);

            var nameToUse = randomizer.Next(resource.listOfProjectUsed.Count);

            // Act
            var result = await _storiesReferencesAccess.GetProjectId(resource.listOfProjectUsed.ElementAtOrDefault(nameToUse));

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void StoriesReferences_GetProjectByIdWhichIsNotThere_ReturnEmpty_Success()
        {
            // Arrange
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, null);

            // Act
            var result = await _storiesReferencesAccess.GetProjectId(NaturalValues.EmptyProjectAcronym);

            // Assert - descriptive
            result.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Get Story/ies

        [Fact]
        public async void StoriesReferences_GetStoryById_Success()
        {
            // Arrange
            var listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToGet);

            // Act
            var result = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToGet, NaturalValues.StoryNumberToUse);

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void StoriesReferences_GetStoryByIdWhichIsNotThere_ReturnEmpty_Success()
        {
            // Arrange
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, null);

            // Act
            var result = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToGet, NaturalValues.StoryNumberToNotGet);

            // Assert - descriptive
            result.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void StoriesReferences_GetProjectStoriesById_Success()
        {
            // Arrange
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToGet);

            // Act
            var result = await _storiesReferencesAccess.GetProjectStoriesIds(NaturalValues.ProjectAcronymToGet);

            // Assert - descriptive
            result.Should().NotBeEmpty()
                .And.HaveCount(resource.listOfStoriesReferenceIds.Count); ;

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }
        #endregion

        #region Remove a Reference
        
        [Fact]
        public async void StoriesReferences_RemoveStoryReference_Success()
        {

            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToGet);

            // Act
            var result = await _storiesReferencesAccess.RemoveReferenceOfStory(resource.listOfStoriesReferenceIds.ElementAtOrDefault(NaturalValues.StoryNumberToUse));

            // Assert - descriptive
            result.Should().BeTrue();

            var resultAfterDelete = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToGet, NaturalValues.StoryNumberToUse);
            resultAfterDelete.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        //TODO: TESTS with different project names, wrong number for project, empty projects
        //TODO: Update Reference acronym
        //TODO: RemoveReference
        //TODO: MakeReferenceForProjectAndStory
        //TODO: StartStoriesReferenceForProject

    }
}
