using Moq;
using ProjectManager;
using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public static class ManagerMocksUtility
    {
        public static Mock<IProjectsAccessProxy> ProjectAccessMock = new Mock<IProjectsAccessProxy>();
        public static Mock<IProjectsMetadataAccessProxy> ProjectsMetadataAccessMock = new Mock<IProjectsMetadataAccessProxy>();
        public static Mock<IStoriesAccessProxy> StoriesAccessMock = new Mock<IStoriesAccessProxy>();
        public static Mock<IStoriesReferencesAccessProxy> StoriesReferencesAccessMock = new Mock<IStoriesReferencesAccessProxy>();

        public static void OpenProjectsSetup(this Mock<IProjectsAccessProxy> mock)
        {
            //TODO: Need a builder for this
            var response = new List<ProjectResponse>();
            mock.Setup(resourceAccess => resourceAccess.OpenProjects())
            .ReturnsAsync(response);
        }

        public static void OpenProjectsWithProjAcrSetup(this Mock<IProjectsAccessProxy> mock, string projectAcronym)
        {
            //TODO: Need a builder for this
            var response = new ProjectResponse();
            mock.Setup(resourceAccess => resourceAccess.OpenProject(projectAcronym))
            .ReturnsAsync(response);
        }

        //TODO: Create nulls functions for the tests cases
    }
}
