using Moq;
using ProjectManager;
using System.Linq;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public static class ManagerMocksUtility
    {

        public static DomainUtilityProjectBuilder domainUtilityBuilder = new DomainUtilityProjectBuilder();

        //GETTO: Create nulls functions for the tests cases
        #region IProjectsAccessProxy
        public static void OpenProjectsSetup(this Mock<IProjectsAccessProxy> mock, int numberOfProjects)
        {
            domainUtilityBuilder = new DomainUtilityProjectBuilder();
            var response = domainUtilityBuilder.BuildMultipleProjects(numberOfProjects);
                        
            mock.Setup(resourceAccess => resourceAccess.OpenProjects())
            .ReturnsAsync(response);
        }

        public static void OpenProjectWithProjAcrSetup(this Mock<IProjectsAccessProxy> mock, string projectAcronym)
        {
            domainUtilityBuilder = new DomainUtilityProjectBuilder();
            var response = domainUtilityBuilder.BuildMultipleProjects(NaturalValues.Single, projectAcronym);

            mock.Setup(resourceAccess => resourceAccess.OpenProject(projectAcronym))
            .ReturnsAsync(response.First());
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
