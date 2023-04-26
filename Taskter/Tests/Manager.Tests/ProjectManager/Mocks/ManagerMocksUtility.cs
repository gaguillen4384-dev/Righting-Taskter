using Moq;
using ProjectManager;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public static class ManagerMocksUtility
    {
        public static Mock<IProjectsAccessProxy> ProjectAccessMock = new Mock<IProjectsAccessProxy>();
        public static Mock<IProjectsMetadataAccessProxy> ProjectsMetadataAccessMock = new Mock<IProjectsMetadataAccessProxy>();
        public static Mock<IStoriesAccessProxy> StoriesAccessMock = new Mock<IStoriesAccessProxy>();
        public static Mock<IStoriesReferencesAccessProxy> StoriesReferencesAccessMock = new Mock<IStoriesReferencesAccessProxy>();
        public static DomainUtilityBuilder domainUtilityBuilder = new DomainUtilityBuilder();

        //GETTO: Create nulls functions for the tests cases
        #region IProjectsAccessProxy
        public static void OpenProjectsSetup(this Mock<IProjectsAccessProxy> mock, int numberOfProjects)
        {
            var response = domainUtilityBuilder.BuildMultipleProjects(numberOfProjects);
                        
            mock.Setup(resourceAccess => resourceAccess.OpenProjects())
            .ReturnsAsync(response);
        }

        public static void OpenProjectsWithProjAcrSetup(this Mock<IProjectsAccessProxy> mock, string projectAcronym)
        {
            //GETTO: Need a domain builder function to follow the appropriate testing pattern.
            var response = new ProjectResponse();
            mock.Setup(resourceAccess => resourceAccess.OpenProject(projectAcronym))
            .ReturnsAsync(response);
        }



        #endregion

        #region IProjectsMetadataAccessProxy

        #endregion

        #region IStoriesAccessProxy

        #endregion

        #region IStoriesReferencesAccessProxy

        #endregion
    }
}
