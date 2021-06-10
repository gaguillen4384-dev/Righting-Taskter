using System;
using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.StoryAccessTests
{
    public class StoriesBuilder : IStoriesBuilder
    {
        private List<StoryCreationRequest> _stories;
        private StoryCreationRequest _story;

        // NEED TO INSTANTIATE THE BUILDER PROPERTIES BEFORE THEY GET USED
        public StoriesBuilder()
        {
            _story = new StoryCreationRequest();
            _stories = new List<StoryCreationRequest>();
        }

        public IStoriesBuilder BuildStoryWithProjectAcronym(string projectAcronym)
        {
            this._story.ProjectAcronym = projectAcronym;
            return this;
        }

        public IStoriesBuilder BuildStoryWithName(string name)
        {
            this._story.Name = name;
            return this;
        }

        public IStoriesBuilder BuildStoryWithStoryNumber(int storyNumber)
        {
            this._story.StoryNumber = storyNumber;
            return this;
        }

        public IStoriesBuilder BuildStoryWithDetails(int numberOfDetails)
        {
            var details = new List<StoryDetail>();
            var randomizer = new Random();
            for (int i = 0; i < numberOfDetails; i++) 
            {
                var lineNumber = randomizer.Next(0, numberOfDetails);
                var storyDetail = new StoryDetail()
                {
                    LevelIndentation = lineNumber,
                    Line = NaturalValues.StoryDetailLine + lineNumber
                };
                details.Add(storyDetail);
            }
            this._story.Details = details;
            return this;
        }

        public IStoriesBuilder BuildStoryWithProjectAcronym(bool isRecurrant)
        {
            this._story.IsRecurrant = isRecurrant;
            return this;
        }

        public IEnumerable<StoryCreationRequest> BuildStoriesOut(int numberOfStories)
        {
            for (int i = 0; i < numberOfStories; i++)
            {
                _stories.Add(new StoriesBuilder()
                    .BuildStoryWithProjectAcronym($"{NaturalValues.ProjectAcronymWorks}{i}")
                    .BuildStoryWithStoryNumber(i)
                    .BuildStoryWithName($"{NaturalValues.StoryName}{i}")
                    .BuildStoryWithDetails(i)
                    .Build());
            }

            return _stories;
        }

        public StoryCreationRequest Build()
        {
            return _story;
        }
    }
}
