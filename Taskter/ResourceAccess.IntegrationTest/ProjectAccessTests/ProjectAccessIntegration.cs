using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectsAccessComponent;
using System;
using Utilities.Taskter.Domain;
using Xunit;

namespace ResourceAccess.IntegrationTest.ProjectAccessTests
{
    public class ProjectAccessIntegration : IClassFixture<ProjectResourceFixture>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProjectAccess _projectAccess;
        private readonly IProjectCreationBuilder _projectBuilder;
        private readonly IProjectUpdateBuilder _updateBuilder;
        private readonly ProjectResourceFixture _fixture;

        public ProjectAccessIntegration(ProjectResourceFixture fixture) 
        {
            _fixture = fixture;
            _serviceProvider = fixture.ServiceProvider;
            // System components
            _projectAccess = _serviceProvider.GetService<IProjectAccess>();

            // Test components
            _projectBuilder = _serviceProvider.GetService<IProjectCreationBuilder>();
            _updateBuilder = _serviceProvider.GetService<IProjectUpdateBuilder>();
        }

        #region Open Projects
        
        /// <summary>
        /// Validates the project access getting ALL projects.
        /// </summary>
        [Fact]
        public async void ProjectAccess_GetAllAvailableProjects_Success()
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

        /// <summary>
        /// Validates the project access getting single project.
        /// </summary>
        [Fact]
        public async void ProjectAccess_GetSingleAvailableProjects_Success()
        {
            // Arrange
            _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);

            // Act
            var result = await _projectAccess.OpenProject(NaturalValues.ProjectAcronymToBeUsed);

            // Assert - descriptive
            result.Should().BeOfType<ProjectResponse>();
            result.As<ProjectResponse>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToBeUsed);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        #endregion

        #region Start Projects
        /// <summary>
        /// Validates the project access getting ALL projects.
        /// </summary>
        [Fact]
        public async void ProjectAccess_CreateASingleProject_Success()
        {
            // Arrange
            var projectToCreate = _projectBuilder.BuildProjectWithName(NaturalValues.ProjectNameToBeUsedForCreation)
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
        public async void ProjectAccess_CreateASingleProject_WithProperNameAndAcronym_Success()
        {
            // Arrange
            var projectToCreate = _projectBuilder.BuildProjectWithName(NaturalValues.ProjectNameToBeUsedForCreation)
                                            .BuildProjectWithProjectAcronym(NaturalValues.ProjectAcronymToBeUsed)
                                            .Build();
            // Act
            var result = await _projectAccess.StartProject(projectToCreate);

            // Assert - descriptive
            result.As<ProjectResponse>().Name.Should().Be(NaturalValues.ProjectNameToBeUsedForCreation);
            result.As<ProjectResponse>().ProjectAcronym.Should().Be(NaturalValues.ProjectAcronymToBeUsed);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: CREATE PROJECT WITH EXPECTED FAILURE WITH WRONG PROPERTY

        //TODO: CREATE ALREADY CREATED PROJECT

        #endregion

        #region Remove Projects

        /// <summary>
        /// Validates the project access removing single project.
        /// </summary>
        [Fact]
        public async void ProjectAccess_RemoveSingleProject()
        {
            // Arrange
            // Create a bunch of projects
            _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);

            // Act
            // Delete project0
            var resultOfDelete = await _projectAccess.RemoveProject(NaturalValues.ProjectAcronymToBeUsed);

            // Assert - descriptive
            // check that the result is true
            resultOfDelete.Should().BeTrue();

            // check by looking for project deleted that is not there.
            var resultAfterDelete = await _projectAccess.OpenProject(NaturalValues.ProjectAcronymToBeUsed);
            resultAfterDelete.Should().BeOfType<EmptyProjectResponse>();

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        //TODO: REMOVE MULTIPLE PROJECTS.

        //TODO: TRY REMOVING PROJECT THAT AINT THERE.

        //TODO: Remove Stories reference that get created.

        #endregion

        #region Update Projects

        /// <summary>
        /// Validates the project access Updating a project.
        /// </summary>
        [Fact]
        public async void ProjectAccess_UpdateSingleProjectName_Success()
        {
            // Arrange
            _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);
            
            var projectToCreate = _updateBuilder.BuildProjectUpdateRequestWithName(NaturalValues.ProjectNameToBeUsedForUpdate)
                                            .Build();

            // Act
            var result = await _projectAccess.UpdateProject(projectToCreate, NaturalValues.ProjectAcronymToBeUsed);

            // Assert - descriptive
            result.As<ProjectResponse>().Name.Should().NotBe(NaturalValues.ProjectNameToBeUsedForCreation);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }

        /// <summary>
        /// Validates the project access Updating a project.
        /// </summary>
        [Fact]
        public async void ProjectAccess_UpdateSingleProjectAcronym_Success()
        {
            // Arrange
            _fixture.PopulateProjectCollection(NaturalValues.NumberOfProjectsToBeCreated);

            var projectToCreate = _updateBuilder.BuildProjectUpdateRequestWithAcronym(NaturalValues.ProjectAcronymToBeUsedForUpdate)
                                            .Build();

            // Act
            var result = await _projectAccess.UpdateProject(projectToCreate, NaturalValues.ProjectAcronymToBeUsed);

            // Assert - descriptive
            result.As<ProjectResponse>().Name.Should().NotBe(NaturalValues.ProjectAcronymToBeUsed);

            // Teardown Needs to happen per test so other tests are not affected.
            _fixture.Dispose();
        }


        #endregion
    }
}
