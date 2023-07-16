using Moq;
using ProjectManager;
using System;
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
            var response = domainUtilityBuilder.BuildMultipleProjectsAndMetadata(numberOfProjects);
                        
            mock.Setup(resourceAccess => resourceAccess.OpenProjects())
            .ReturnsAsync(response);
        }

        public static void OpenProjectsWithProjAcrSetup(this Mock<IProjectsAccessProxy> mock)
        {
            //GETTO: Need a domain builder function to follow the appropriate testing pattern.
            var response = domainUtilityBuilder.BuildSingleProject();

            mock.Setup(resourceAccess => resourceAccess.OpenProject(It.IsAny<string>()))
            .ReturnsAsync(response);
        }

        public static void CreateProjectSetup(this Mock<IProjectsAccessProxy> mock) 
        {
            var response = domainUtilityBuilder.BuildSingleProject();

            mock.Setup(resourceAccess => resourceAccess.StartProject(It.IsAny<ProjectCreationRequest>()))
            .ReturnsAsync(response);
        }

        //GETTO: Figure out how to test the update of project and metadata.

        #endregion

        #region IProjectsMetadataAccessProxy

        public static void GetCurrentMetadataForProjects(this Mock<IProjectsMetadataAccessProxy> mock) 
        {
            var response = domainUtilityBuilder.GetMultipleProjectsWithMetadata();

            mock.Setup(resourceAccess => resourceAccess.GetAllProjectsMetadataDetails())
            .ReturnsAsync(response);
        }

        public static void GetCurrentMetadataForProject(this Mock<IProjectsMetadataAccessProxy> mock, string projectAcronym)
        {
            var response = domainUtilityBuilder.GetMultipleProjectsWithMetadata();

            mock.Setup(resourceAccess => resourceAccess.GetProjectMetadataDetails(projectAcronym))
            .ReturnsAsync(response.First());
        }

        #endregion

        #region IStoriesAccessProxy

        #endregion

        #region IStoriesReferencesAccessProxy

        #endregion
    }
}
