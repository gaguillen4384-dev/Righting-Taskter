using ProjectsMetadataAccessComponent;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public interface IProjectMetadataBuilder
    {
        IProjectMetadataBuilder BuildStoryWithName(string name);

        IProjectMetadataBuilder BuildStoryWithStoryNumber(int storyNumber);

        IProjectMetadataBuilder BuildStoryWithDetails(int numberOfDetails);

        IProjectMetadataBuilder BuildStoryWithIsRecurrant(bool flag);

        IProjectMetadataBuilder UpdateStoryWithName(string name);

        IProjectMetadataBuilder UpdateStoryWithDetails(int numberOfDetails);

        IProjectMetadataBuilder UpdateStoryWithIsCompleted(bool flag);

        IProjectMetadataBuilder UpdateStoryWithIsRecurrant(bool flag);

        IEnumerable<ProjectMetadataDocument> BuildProjectsOut(int numberOfStories);

        ProjectMetadataDocument BuildCreateRequest();
    }
}
