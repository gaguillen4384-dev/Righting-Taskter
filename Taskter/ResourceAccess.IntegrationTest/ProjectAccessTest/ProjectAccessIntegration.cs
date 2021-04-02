using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectAccessComponent;
using System;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectAccessIntegration : IClassFixture<ProjectResourceFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProjectAccess _projectAccess;
        private readonly IProjectCreationBuilder _projectBuilder;
        private readonly ProjectResourceFixture _fixture;

        public ProjectAccessIntegration(ProjectResourceFixture fixture) 
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components
            _projectAccess = _serviceProvider.GetService<IProjectAccess>();

            // Test components
            _projectBuilder = _serviceProvider.GetService<IProjectCreationBuilder>();
        }

        #region Open Projects
        /// <summary>
        /// Validates the project access getting ALL projects.
        /// </summary>
        [Fact]
        public async void ProjectAccess_GetAllAvailableProjects()
        {
            // Arrange
            _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);

            // Act
            var result = await _projectAccess.OpenProjects();

            // Assert - descriptive
            result.Should().NotBeEmpty()
                .And.HaveCount(NaturalValues.NumberOfProjectsToBeCreated);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Start Projects
        /// <summary>
        /// Validates the project access getting ALL projects.
        /// </summary>
        [Fact]
        public async void ProjectAccess_CreateASingleProject()
        {
            // Arrange
            var projectToCreate = _projectBuilder.BuildProjectWithName(NaturalValues.ProjectNameToBeUsed)
                                            .BuildProjectWithProjectAcronym(NaturalValues.ProjectAcronymToBeUsed)
                                            .Build();
            // Act
            var result = await _projectAccess.StartProject(projectToCreate);

            // Assert - descriptive
            result.Should().NotBeNull()
                .And.BeOfType(typeof(ProjectResponse));


            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        /// <summary>
        /// Validates the project access getting ALL projects.
        /// </summary>
        [Fact]
        public async void ProjectAccess_CreateASingleProject_WithProperName()
        {
            // Arrange
            var projectToCreate = _projectBuilder.BuildProjectWithName(NaturalValues.ProjectNameToBeUsed)
                                            .BuildProjectWithProjectAcronym(NaturalValues.ProjectAcronymToBeUsed)
                                            .Build();
            // Act
            var result = await _projectAccess.StartProject(projectToCreate);

            // Assert - descriptive
            result.As<ProjectResponse>().Name.Should().Be(NaturalValues.ProjectNameToBeUsed);
            result.As<ProjectResponse>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToBeUsed);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }
        #endregion
    }
}
