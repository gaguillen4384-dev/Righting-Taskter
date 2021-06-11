using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public interface IStoriesBuilder
    {
        IStoriesBuilder BuildStoryWithName(string name);

        IStoriesBuilder BuildStoryWithStoryNumber(int storyNumber);

        IStoriesBuilder BuildStoryWithDetails(int numberOfDetails);

        IEnumerable<StoryCreationRequest> BuildStoriesOut(int numberOfStories);

        IStoriesBuilder BuildStoryWithProjectAcronym(bool isRecurrant);

        StoryCreationRequest Build();
    }
}
