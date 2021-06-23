using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.StoriesReferencesAccessTests
{
    public class FixtureResource
    {
        public HashSet<string> listOfStoriesReferenceIds { get; set; } = new HashSet<string>();

        public HashSet<string> listOfProjectIds { get; set; } = new HashSet<string>();

        public HashSet<string> listOfProjectUsed { get; set; } = new HashSet<string>();

        public HashSet<string> listOfStoriesIds { get; set; } = new HashSet<string>();
    }
}