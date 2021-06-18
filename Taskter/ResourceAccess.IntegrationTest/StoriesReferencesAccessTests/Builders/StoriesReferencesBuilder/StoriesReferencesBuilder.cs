﻿using StoriesReferencesAccessComponent;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.StoriesReferencesAccessTests
{
    public class StoriesReferencesBuilder : IStoriesReferencesBuilder
    {
        private StoryReferenceDocument _storyReference;
        private List<StoryReferenceDocument> _storyReferences;

        public StoriesReferencesBuilder() 
        {
            _storyReference = new StoryReferenceDocument();
            _storyReferences = new List<StoryReferenceDocument>();
        }

        public IStoriesReferencesBuilder BuildStoryReferenceWithIsDeleted(bool isDeleted)
        {
            _storyReference.IsDeleted = isDeleted;
            return this;
        }

        public IStoriesReferencesBuilder BuildStoryReferenceWithProjectAcronym(string projectAcronym)
        {
            _storyReference.ProjectAcronym = projectAcronym;
            return this;
        }

        public IStoriesReferencesBuilder BuildStoryReferenceWithProjectId(string projectId)
        {
            _storyReference.ProjectId = projectId;
            return this;
        }

        public IStoriesReferencesBuilder BuildStoryReferenceWithStoryId(string storyId)
        {
            _storyReference.StoryId = storyId;
            return this;
        }

        public IStoriesReferencesBuilder BuildStoryReferenceWithStoryNumber(int storyNumber)
        {
            _storyReference.StoryNumber = storyNumber;
            return this;
        }

        public StoryReferenceDocument Build()
        {
            return this._storyReference;
        }

        public IEnumerable<StoryReferenceDocument> BuildStoriesReferences(int numberOfReferences)
        {
            for (int i = 0; i < numberOfReferences; i++) 
            {
                //TODO: replace with natural values.
                _storyReferences.Add(new StoriesReferencesBuilder()
                    .BuildStoryReferenceWithProjectAcronym($"PJT{i}")
                    .BuildStoryReferenceWithStoryNumber(i)
                    .BuildStoryReferenceWithStoryId($"PJT{i}")
                    .BuildStoryReferenceWithIsDeleted(false)
                    .BuildStoryReferenceWithProjectId($"PJT{i}")
                    .Build());
            }

            return _storyReferences;
        }
    }
}
