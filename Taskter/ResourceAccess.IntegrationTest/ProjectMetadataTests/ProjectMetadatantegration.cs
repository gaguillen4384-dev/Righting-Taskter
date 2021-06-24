using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsMetadataAccessComponent;
using System;
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

        /// <summary>
        /// Reads a story from a project. This is one is not specific, limitations of the system.
        /// </summary>
        [Fact]
        public async void StoriesAccess_GetAllProjectsMetadata_Success()
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
        //TODO:CreateProjectMetadataDetails
        //TODO:GetProjectMetadataDetails
        
        //TODO:UpdateProjectMetadataDetails, false
        //TODO:UpdateProjectMetadataDetails, true
        //TODO:UpdateProjectMetadataAcronym
        //TODO:RemoveProjectMetadataDetails
        //TODO:GetLatestStoryNumberForProject


    }
}
