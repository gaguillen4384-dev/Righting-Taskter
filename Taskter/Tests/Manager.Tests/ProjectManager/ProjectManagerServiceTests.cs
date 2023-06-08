using FluentAssertions;
using Moq;
using ProjectManager;
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

        #region Get Projects
        [Fact]
        public async void ProjectManager_GetAllProjects_Success()
        {
            // Arrange
            // This showcases how extension classes can be used to setup mocks.
            // Within each setup theres a builder call, which takes in parameters.
            _projectAccessMock.OpenProjectsSetup(NaturalValues.NumberOfProjectsToUse);
            _projectsMetadataAccessMock.GetCurrentMetadataForProjects();

            // Act
            //GETTO: test out the sqllitedriver and see if it works since its missing a lot of stuff.
            var result = await _projectManager.GetProjects();

            // Assert - descriptive
            result.Should().NotBeEmpty();

            // Teardown Needs to happen per test so other tests are not affected. This is relevant for integration tests.
            // Or mocks gets reset per setup method.
        }

        [Fact]
        public async void ProjectManager_GetSingleProjects_Success()
        {
            // Arrange
            _projectAccessMock.OpenProjectWithProjAcrSetup(NaturalValues.PrjAcronymToUse);
            _projectsMetadataAccessMock.GetCurrentMetadataForProject(NaturalValues.PrjAcronymToUse);

            // Act
            //GETTO: missing validation for metadata stuff that the response has.
            var result = await _projectManager.GetProject(NaturalValues.PrjAcronymToUse);

            // Assert - descriptive
            result.Should().NotBeNull();
            result.ProjectAcronym.Should().Be(NaturalValues.PrjAcronymToUse);
        }

        //GETTO: test metadata in a predictiable way, may not just null but also numeric or if want to naturalvalues.

        #endregion

        //GETTO: figure out how to test prject metadata and stories reference. without making it integration test.
        #region Create Project

        [Fact]
        public async void ProjectManager_CreateProject_Success()
        {
            // Arrange
            var domainUtilityBuilder = new DomainUtilityDTOBuilder();
            var newProjectRequest = domainUtilityBuilder.BuildNewProjectRequest();
            _projectAccessMock.CreateProjectWithGuid(newProjectRequest);

            // Act
            var result = await _projectManager.CreateProject(newProjectRequest);

            // Assert - descriptive
            result.Should().NotBeNull().And.Be(NaturalValues.PrjAcronymToUse);
        }

        #endregion

        #region Edit Project

        [Fact]
        public async void ProjectManager_EditProject_Success()
        {
            // Arrange
            var domainUtilityBuilder = new DomainUtilityDTOBuilder();
            //GETTO: setup for update project. Make a requestobject to change with a particula prjtacr
            //var newProjectRequest = domainUtilityBuilder.BuildNewProjectRequest();
            //_projectAccessMock.CreateProjectWithGuid(newProjectRequest);

            // Act
            //GETTO: use the updateproject method.
            //var result = await _projectManager.CreateProject(newProjectRequest);

            // Assert - descriptive
            //GETTO: See if metadata and other ish also got updated?
            //result.Should().NotBeNull().And.Be(NaturalValues.PrjGuid.ToString());
        }

        #endregion

        //GETTO: RemoveProject
        //GETTO: CreateStory
    }
}
