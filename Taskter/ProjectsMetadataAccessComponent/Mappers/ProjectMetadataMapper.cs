using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ProjectsMetadataAccessComponent
{
    /// <summary>
    /// Responsible for mapping project metadata domain to project reponses/request.
    /// </summary>
    public static class ProjectMetadataMapper
    {
        public static ProjectMetadataDetails MapToProjectMetadataDetails(ProjectMetadataDocument projectsStoryNumber)
        {
            return new ProjectMetadataDetails()
            {
                ProjectAcronym = projectsStoryNumber.ProjectAcronym,
                LatestStoryNumber = projectsStoryNumber.LatestStoryNumber,
                NumberOfActiveStories = projectsStoryNumber.NumberOfActiveStories,
                NumberOfStoriesCompleted = projectsStoryNumber.NumberOfStoriesCompleted
            };
        }

        public static IEnumerable<ProjectMetadataDetails> MapToProjectsMetadataDetails(IEnumerable<ProjectMetadataDocument> projectsDetails)
        {
            var listOfProjectsNumbers = new List<ProjectMetadataDetails>();

            foreach (var projectMetadata in projectsDetails)
            {
                listOfProjectsNumbers.Add(MapToProjectMetadataDetails(projectMetadata));
            }

            return listOfProjectsNumbers;
        }


        public static ProjectMetadataDetails MapToEmptyProjectMetadataDetails()
        {
            return new EmptyProjectMetadataDetails();
        }
    }
}
