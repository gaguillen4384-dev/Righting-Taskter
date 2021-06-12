using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public interface IStoriesBuilder
    {
        IStoriesBuilder BuildStoryWithName(string name);

        IStoriesBuilder BuildStoryWithStoryNumber(int storyNumber);

        IStoriesBuilder BuildStoryWithDetails(int numberOfDetails);

        IStoriesBuilder BuildStoryWithIsRecurrant(bool flag);

        IStoriesBuilder UpdateStoryWithName(string name);

        IStoriesBuilder UpdateStoryWithDetails(int numberOfDetails);

        IStoriesBuilder UpdateStoryWithIsCompleted(bool flag);

        IStoriesBuilder UpdateStoryWithIsRecurrant(bool flag);

        IEnumerable<StoryCreationRequest> BuildStoriesOut(int numberOfStories);

        StoryCreationRequest BuildCreateRequest();

        StoryUpdateRequest BuildUpdateRequest();
    }
}
