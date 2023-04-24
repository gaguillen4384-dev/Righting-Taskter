using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsMetadataAccessComponent;
using System;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public class ProjectMetadataFixtureIntegration : IClassFixture<ProjectMetadataFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProjectsMetadataAccess _projectMetadataAccess;
        private readonly IProjectMetadataBuilder _projectMetadataBuilder;
        private readonly ProjectMetadataFixture _fixture;
        private readonly Random randomizer = new Random();

        public ProjectMetadataFixtureIntegration(ProjectMetadataFixture fixture)
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components : Only works because of the Microsoft.Extensions.DependencyInjection
            _projectMetadataAccess = _serviceProvider.GetService<IProjectsMetadataAccess>();

            //// Test components
            _projectMetadataBuilder = _serviceProvider.GetService<IProjectMetadataBuilder>();
        }



        #region Get Project Metadata

        [Fact]
        public async void ProjectMetadata_GetAllProjectsMetadata_Success()
        {
            // Arrange
            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate);

            // Act
            var result = await _projectMetadataAccess.GetAllProjectsMetadataDetails();

            // Assert - descriptive
            result.Should().NotBeEmpty()
                .And.HaveCount(NaturalValues.NumberOfProjectsToPopulate);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void ProjectMetadata_GetProjectMetadata_Success()
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();

                            
            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeNull().And.BeOfType<ProjectMetadataDetails>();
            result.As<ProjectMetadataDetails>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToUse);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void ProjectMetadata_GetLatestStoryNumberForProject_Success()
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            var result = await _projectMetadataAccess.GetLatestStoryNumberForProject(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().Be(NaturalValues.StoryNumbeToUse);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Create Project Metadata
        
        [Fact]
        public async void ProjectMetadata_CreateProjectMetadata_Success()
        {
            // Arrange

            // Act
            var created = await _projectMetadataAccess.CreateProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeNull();
            result.As<ProjectMetadataDetails>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToUse);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Update Project Metadata  

        [Fact]
        public async void ProjectMetadata_UpdateProjectMetadataDetails_OnlyOneNewStory_isCompletedFalse_Success() 
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            await _projectMetadataAccess.UpdateProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeNull();
            result.As<ProjectMetadataDetails>().NumberOfStoriesCompleted.Should().Be(NaturalValues.NumberOfCompletedStories);
            result.As<ProjectMetadataDetails>().NumberOfActiveStories.Should().Be(NaturalValues.NumberOfActiveStories+1);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void ProjectMetadata_UpdateProjectMetadataDetails_OnlyOneStory_isCompletedTrue_Success()
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            await _projectMetadataAccess.UpdateProjectMetadataDetails(NaturalValues.ProjectAcronymToUse, NaturalValues.IsCompleted);

            // Assert - descriptive
            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeNull();
            result.As<ProjectMetadataDetails>().NumberOfStoriesCompleted.Should().Be(NaturalValues.NumberOfCompletedStories+1);
            result.As<ProjectMetadataDetails>().NumberOfActiveStories.Should().Be(NaturalValues.NumberOfActiveStories);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void ProjectMetadata_UpdateProjectMetadataAcronym_NameFound_Success()
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            await _projectMetadataAccess.UpdateProjectMetadataAcronym(NaturalValues.ProjectAcronymToUse, NaturalValues.ProjectAcronymToUpdate);

            // Assert - descriptive
            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUpdate);

            // Assert - descriptive
            result.Should().NotBeNull();
            result.As<ProjectMetadataDetails>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToUpdate);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        [Fact]
        public async void ProjectMetadata_UpdateProjectMetadataAcronym_NameUsedNotFound_Success()
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            await _projectMetadataAccess.UpdateProjectMetadataAcronym(NaturalValues.ProjectAcronymToUse, NaturalValues.ProjectAcronymToUpdate);

            // Assert - descriptive
            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().BeOfType<EmptyProjectMetadataDetails>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Remove Project Metadata
        
        [Fact]
        public async void ProjectMetadata_RemoveProjectMetadataDetails_Success() 
        {
            // Arrange
            var request = _projectMetadataBuilder
                            .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse)
                            .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories)
                            .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories)
                            .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                            .BuildCreateRequest();


            var listOfIds = _fixture.PopulateProjectMetadataCollection(NaturalValues.NumberOfProjectsToPopulate, request);

            // Act
            await _projectMetadataAccess.RemoveProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            // Act
            var result = await _projectMetadataAccess.GetProjectMetadataDetails(NaturalValues.ProjectAcronymToUse);

            // Assert - descriptive
            result.Should().BeOfType<EmptyProjectMetadataDetails>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion


    }
}
