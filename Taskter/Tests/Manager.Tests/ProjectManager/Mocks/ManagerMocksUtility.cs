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

        public static void OpenProjectWithProjAcrSetup(this Mock<IProjectsAccessProxy> mock, string projectAcronym)
        {
            domainUtilityBuilder = new DomainUtilityProjectBuilder();
            var response = domainUtilityBuilder.BuildMultipleProjectsAndMetadata(NaturalValues.Single, projectAcronym);

            mock.Setup(resourceAccess => resourceAccess.OpenProject(projectAcronym))
            .ReturnsAsync(response.First());
        }

        public static void CreateProjectWithGuid(this Mock<IProjectsAccessProxy> mock, ProjectCreationRequest projectCreationRequest) 
        {
            //GETTO: What needs to be return is not the guid but the project acronym passed in. 
            domainUtilityBuilder = new DomainUtilityProjectBuilder();
            var response = domainUtilityBuilder.BuildProjectWithGuidSetup(guid.ToString());

            mock.Setup(resourceAccess => resourceAccess.StartProject(projectCreationRequest))
            .ReturnsAsync(response.First());
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
