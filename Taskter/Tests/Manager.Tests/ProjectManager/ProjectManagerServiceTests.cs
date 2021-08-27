using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProjectManager;
using ProjectsAccessComponent;
using ProjectsMetadataAccessComponent;
using StoriesAccessComponent;
using StoriesReferencesAccessComponent;
using System;
using Xunit;

namespace Manager.Tests.ProjectManager
{
    public class ProjectManagerServiceTests 
    {
        private IProjectManagerService _projectManager;
        private Mock<IProjectsAccessProxy> _projectAccessMock;
        private Mock<IProjectsMetadataAccessProxy> _projectsMetadataAccessMock;
        private Mock<IStoriesAccessProxy> _storiesAccessMock;
        private Mock<IStoriesReferencesAccessProxy> _storiesReferencesAccessMock;

        // TODO: These tests should reflect validation that the manager has.
        public ProjectManagerServiceTests() 
        {
            _projectAccessMock = new Mock<IProjectsAccessProxy>();
            _projectsMetadataAccessMock = new Mock<IProjectsMetadataAccessProxy>();
            _storiesAccessMock = new Mock<IStoriesAccessProxy>();
            _storiesReferencesAccessMock = new Mock<IStoriesReferencesAccessProxy>();
            // This work by reference, so when the mocks get updated so does the projectmanager.
            _projectManager = new ProjectManagerService(_projectAccessMock.Object, _storiesAccessMock.Object, _storiesReferencesAccessMock.Object, _projectsMetadataAccessMock.Object);
        }

        //TODO: Finish this test, on doing so TODOS will disapper.
        [Fact]
        public async void ProjectManager_GetAllProjects_Success()
        {
            // Arrange
            // This showcases how extension classes can be used to setup mocks.
            // Within each setup theres a builder call, which takes in parameters.
            _projectAccessMock.OpenProjectsSetup(NaturalValues.numberOfProjectsToUse);

            // Act
            var result = await _projectManager.GetProjects();

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
        }
    }
}
