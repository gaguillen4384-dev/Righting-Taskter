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

        // GETTO: These tests get to have negative version where validation is tested
        //      Validation being the responsability of the manager.
        public ProjectManagerServiceTests() 
        {
            _projectAccessMock = new Mock<IProjectsAccessProxy>();
            _projectsMetadataAccessMock = new Mock<IProjectsMetadataAccessProxy>();
            _storiesAccessMock = new Mock<IStoriesAccessProxy>();
            _storiesReferencesAccessMock = new Mock<IStoriesReferencesAccessProxy>();
            // This work by reference, so when the mocks get updated so does the projectmanager.
            _projectManager = new ProjectManagerService(_projectAccessMock.Object, _storiesAccessMock.Object, _storiesReferencesAccessMock.Object, _projectsMetadataAccessMock.Object);
        }

        [Fact]
        public async void ProjectManager_GetAllProjects_Success()
        {
            // Arrange
            // This showcases how extension classes can be used to setup mocks.
            // Within each setup theres a builder call, which takes in parameters.
            _projectAccessMock.OpenProjectsSetup(NaturalValues.numberOfProjectsToUse);

            // Act
            //GETTO: test out the sqllitedriver and see if it works since its missing a lot of stuff.
            var result = await _projectManager.GetProjects();

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected.
        }

        //GETTO: CreateProject
        //GETTO: GetProject
        //GETTO: EditProject
        //GETTO: CreateStory
    }
}
