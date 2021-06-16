using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public interface IStoriesReferencesBuilder
    {
        IStoriesReferencesBuilder BuildStoryWithName(string name);

        IStoriesReferencesBuilder BuildStoryWithStoryNumber(int storyNumber);

        IStoriesReferencesBuilder BuildStoryWithDetails(int numberOfDetails);

        IStoriesReferencesBuilder BuildStoryWithIsRecurrant(bool flag);

        IStoriesReferencesBuilder UpdateStoryWithName(string name);

        IStoriesReferencesBuilder UpdateStoryWithDetails(int numberOfDetails);

        IStoriesReferencesBuilder UpdateStoryWithIsCompleted(bool flag);

        IStoriesReferencesBuilder UpdateStoryWithIsRecurrant(bool flag);

        IEnumerable<StoryCreationRequest> BuildStoriesOut(int numberOfStories);

        StoryCreationRequest BuildCreateRequest();

        StoryUpdateRequest BuildUpdateRequest();
    }
}
