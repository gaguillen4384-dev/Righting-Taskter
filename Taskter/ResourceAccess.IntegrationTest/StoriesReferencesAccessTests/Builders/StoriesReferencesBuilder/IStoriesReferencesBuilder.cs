using StoriesReferencesAccessComponent;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.StoriesReferencesAccessTests
{
    public interface IStoriesReferencesBuilder
    {
        IStoriesReferencesBuilder BuildStoryReferenceWithProjectAcronym(string projectAcronym);
        
        IStoriesReferencesBuilder BuildStoryReferenceWithProjectId(string projectId);

        IStoriesReferencesBuilder BuildStoryReferenceWithStoryId(string storyId);

        IStoriesReferencesBuilder BuildStoryReferenceWithStoryNumber(int storyNumber);

        IStoriesReferencesBuilder BuildStoryReferenceWithIsDeleted(bool isDeleted);

        IEnumerable<StoryReferenceDocument> BuildStoriesReferences(int numberOfReferences);
        StoryReferenceDocument Build();
    }
}
