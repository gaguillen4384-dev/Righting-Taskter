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

        IProjectMetadataBuilder IProjectMetadataBuilder.BuildStoryWithName(string name)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.BuildStoryWithStoryNumber(int storyNumber)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.BuildStoryWithDetails(int numberOfDetails)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.BuildStoryWithIsRecurrant(bool flag)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.UpdateStoryWithName(string name)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.UpdateStoryWithDetails(int numberOfDetails)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.UpdateStoryWithIsCompleted(bool flag)
        {
            throw new NotImplementedException();
        }

        IProjectMetadataBuilder IProjectMetadataBuilder.UpdateStoryWithIsRecurrant(bool flag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectMetadataDocument> BuildProjectsOut(int numberOfStories)
        {
            return _projectsMetadata;
        }

        public ProjectMetadataDocument BuildCreateRequest()
        {
            return _projectMetadataToCreate;
        }

        #region Private methods

        #endregion
    }
}
