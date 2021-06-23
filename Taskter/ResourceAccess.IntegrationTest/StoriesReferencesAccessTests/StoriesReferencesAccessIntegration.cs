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
            var listOfIds = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToUse);

            // Act
            var result = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUse, NaturalValues.StoryNumberToUse);

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
            var result = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUse, NaturalValues.StoryNumberToNotGet);

            // Assert - descriptive
            result.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void StoriesReferences_GetProjectStoriesById_Success()
        {
            // Arrange
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToUse);

            // Act
            var result = await _storiesReferencesAccess.GetProjectStoriesIds(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeEmpty()
                .And.HaveCount(resource.listOfStoriesReferenceIds.Count); ;

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: Try to get story with wrong ProjectAcronym
        #endregion

        #region Remove a Reference
        
        [Fact]
        public async void StoriesReferences_RemoveStoryReference_Success()
        {

            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToUse);

            // Act
            var result = await _storiesReferencesAccess.RemoveReferenceOfStory(resource.listOfStoriesReferenceIds.ElementAtOrDefault(NaturalValues.StoryNumberToUse));

            // Assert - descriptive
            result.Should().BeTrue();

            var resultAfterDelete = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUse, NaturalValues.StoryNumberToUse);
            resultAfterDelete.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: Remove something not there.

        #endregion

        #region Make Reference For Project and Story 

        [Fact]
        public async void StoriesReferences_StartAProjectRefAndStoryRef_Success()
        {
            // Act
            await _storiesReferencesAccess.MakeReferenceForStoryInProject(NaturalValues.ProjectAcronymToUse, 
                            NaturalValues.StoryNumberToUse, 
                            NaturalValues.SingleStoryId,
                            NaturalValues.SingleProjectId);

            // Assert - descriptive
            var resultAfterCreation = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUse, NaturalValues.StoryNumberToUse);
            resultAfterCreation.Should().NotBeEmpty();
            resultAfterCreation.Should().BeEquivalentTo(NaturalValues.SingleStoryId);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: test for a projectAcronym not being there, Should manager be validating this or RA?

        #endregion

        #region Update References

        [Fact]
        public async void StoriesReferences_UpdateRef_Success()
        {
            var resource = _fixture.PopulateStoriesCollection(NaturalValues.NumberOfStoryRefToCreate, NaturalValues.ProjectAcronymToUse);
            var storyId = resource.listOfStoriesIds.ElementAtOrDefault(NaturalValues.StoryNumberToUse);
            var projectId = resource.listOfProjectIds.FirstOrDefault();

            // Act
            await _storiesReferencesAccess.UpdateStoryReferenceAcronym(NaturalValues.ProjectAcronymToUpdateWith, projectId);

            // Assert - descriptive
            var projectAfterUpdate = await _storiesReferencesAccess.GetProjectId(NaturalValues.ProjectAcronymToUpdateWith);
            projectAfterUpdate.Should().NotBeEmpty();
            projectAfterUpdate.Should().BeEquivalentTo(projectId);

            var storyAfterCreation = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUpdateWith, NaturalValues.StoryNumberToUse);
            storyAfterCreation.Should().NotBeEmpty();
            storyAfterCreation.Should().BeEquivalentTo(storyId);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: One where we see if we can find previous prj acronym assert for empty

        #endregion

        #region Start stories reference for project

        [Fact]
        public async void StoriesReferences_StartAProjectRef_Success()
        {

            // Act
            await _storiesReferencesAccess.StartStoriesReferenceForProject(NaturalValues.ProjectAcronymToUse, NaturalValues.SingleProjectId);

            // Assert - descriptive
            var projectAfterUpdate = await _storiesReferencesAccess.GetProjectId(NaturalValues.ProjectAcronymToUse);
            projectAfterUpdate.Should().NotBeEmpty();
            projectAfterUpdate.Should().BeEquivalentTo(NaturalValues.SingleProjectId);

            // The reference so far has no new story, no default story.
            var storyAfterCreation = await _storiesReferencesAccess.GetSingleStoryId(NaturalValues.ProjectAcronymToUse, NaturalValues.StoryNumberToUse);
            storyAfterCreation.Should().BeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: Make sure I can still find other porjects even if created another one.

        #endregion
    }
}
