using ProjectsMetadataAccessComponent;
using System;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.ProjectMetadataTests
{
    public class ProjectMetadataBuilder : IProjectMetadataBuilder
    {
        private List<ProjectMetadataDocument> _projectsMetadata;
        private ProjectMetadataDocument _projectMetadataToCreate;

        // NEED TO INSTANTIATE THE BUILDER PROPERTIES BEFORE THEY GET USED
        public ProjectMetadataBuilder()
        {
            _projectsMetadata = new List<ProjectMetadataDocument>();
            _projectMetadataToCreate = new ProjectMetadataDocument();
        }

        public IProjectMetadataBuilder BuildrojectMetadataWithLatestStoryNumber(int latestStoryNumber)
        {
            _projectMetadataToCreate.LatestStoryNumber = latestStoryNumber;
            return this;
        }

        public IProjectMetadataBuilder BuildrojectMetadataWithNumberOfActiveStories(int numberOfActiveStories)
        {
            _projectMetadataToCreate.NumberOfActiveStories = numberOfActiveStories;
            return this;
        }

        public IProjectMetadataBuilder BuildrojectMetadataWithNumberOfStoriesCompleted(int numberOfStoriesCompleted)
        {
            _projectMetadataToCreate.NumberOfStoriesCompleted = numberOfStoriesCompleted;
            return this;
        }

        public IProjectMetadataBuilder BuildrojectMetadataWithProjectAcronym(string projectAcronym)
        {
            _projectMetadataToCreate.ProjectAcronym = projectAcronym;
            return this;
        }

        public ProjectMetadataDocument BuildCreateRequest()
        {
            return _projectMetadataToCreate;
        }

        public IEnumerable<ProjectMetadataDocument> BuildManyProjectsOut(int numberOfProjects)
        {
            for (int i = 0; i < numberOfProjects; i++)
            {
                _projectsMetadata.Add(new ProjectMetadataBuilder()
                    .BuildrojectMetadataWithProjectAcronym(NaturalValues.ProjectAcronymToUse+i)
                    .BuildrojectMetadataWithNumberOfStoriesCompleted(NaturalValues.NumberOfCompletedStories+i)
                    .BuildrojectMetadataWithNumberOfActiveStories(NaturalValues.NumberOfActiveStories+i)
                    .BuildrojectMetadataWithLatestStoryNumber(NaturalValues.StoryNumbeToUse+i)
                    .BuildCreateRequest());
            }

            return _projectsMetadata;
        }

        #region Private methods

        #endregion
    }
}
