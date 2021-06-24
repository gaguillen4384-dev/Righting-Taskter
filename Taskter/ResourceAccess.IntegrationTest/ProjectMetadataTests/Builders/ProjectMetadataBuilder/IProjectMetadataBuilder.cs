using ProjectsMetadataAccessComponent;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public interface IProjectMetadataBuilder
    {
        IProjectMetadataBuilder BuildrojectMetadataWithProjectAcronym(string projectAcronym);

        IProjectMetadataBuilder BuildrojectMetadataWithLatestStoryNumber(int latestStoryNumber);

        IProjectMetadataBuilder BuildrojectMetadataWithNumberOfStoriesCompleted(int numberOfStoriesCompleted);

        IProjectMetadataBuilder BuildrojectMetadataWithNumberOfActiveStories(int numberOfActiveStories);

        IEnumerable<ProjectMetadataDocument> BuildManyProjectsOut(int numberOfProjects);

        ProjectMetadataDocument BuildCreateRequest();
    }
}
