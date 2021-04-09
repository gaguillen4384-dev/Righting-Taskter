using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Taskter.Domain;
using Utilities.Taskter.Domain.Documents;

namespace ProjectAccessComponent
{
    /// <summary>
    /// Responsible for mapping Project domain to project reponses/request.
    /// </summary>
    public static class ProjectRepositoryMapper
    {
        /// <summary>
        /// Maps a project document to a project response.
        /// </summary>
        public static ProjectResponse MapToProjectResponse(ProjectDocument project, ProjectNumbersDetails projectDetails) 
        {
            if (project == null)
                return new EmptyProjectResponse();

            return new ProjectResponse()
            {
                Name = project.Name,
                LastWorkedOn = project.LastWorkedOn,
                DateCreated = project.DateCreated,
                DateUpdated = project.DateUpdated,
                ProjectAcronym = project.ProjectAcronym,
                NumberOfActiveStories = projectDetails.NumberOfActiveStories,
                NumberOfCompletedStories = projectDetails.NumberOfStoriesCompleted
            };
        }

        public static IEnumerable<ProjectResponse> MapToProjectsResponse(IEnumerable<ProjectDocument> projects, IEnumerable<ProjectNumbersDetails> projectsDetails) 
        {
            // TODO: Get a dictionary where the projectAcronym are the same and then map them together
            // then use the MapToProjectResponse in a forloop
            var listOfProjects = new List<ProjectResponse>();
            var projectDictionary = CombineListsToDictionary(projects, projectsDetails);

            foreach (var project in projectDictionary)
            {
                listOfProjects.Add(MapToProjectResponse(project.Key, project.Value));
            }

            return listOfProjects;
        }

        public static ProjectDocument MapToProjectDocumentFromCreationRequest(ProjectCreationRequest projectRequest)
        {
            return new ProjectDocument()
            {
                Name = projectRequest.Name,
                ProjectAcronym = projectRequest.ProjectAcronym
            };
        }

        public static ProjectDocument MapToProjectDocumentFromUpdateRequest(ProjectDocument project, ProjectUpdateRequest projectRequest)
        {
            // map from the original project.
            project.Name = IsProjectNameUpdated(projectRequest) ? projectRequest.Name : project.Name;
            project.ProjectAcronym = IsProjectAcronymUpdated(projectRequest, project.ProjectAcronym) ? projectRequest.ProjectAcronym : project.ProjectAcronym;
            project.DateUpdated = DateTime.UtcNow;
            project.LastWorkedOn = DateTime.UtcNow;

            return project;
        }

        public static ProjectDocument MapToProjectDetailsFromUpdateRequest(ProjectDocument project, ProjectUpdateRequest projectRequest)
        {
            // map from the original project.
            project.Name = IsProjectNameUpdated(projectRequest) ? projectRequest.Name : project.Name;
            project.ProjectAcronym = IsProjectAcronymUpdated(projectRequest, project.ProjectAcronym) ? projectRequest.ProjectAcronym : project.ProjectAcronym;
            project.DateUpdated = DateTime.UtcNow;
            project.LastWorkedOn = DateTime.UtcNow;

            return project;
        }

        /// <summary>
        /// Maps an empty project.
        /// </summary>
        public static ProjectResponse MapToEmptyProjectResponse()
        {
            return new EmptyProjectResponse()
            {
            };
        }

        /// <summary>
        /// Assumes that the request has a projectAcronym if desired changed.
        /// </summary>
        public static bool IsProjectAcronymUpdated(ProjectUpdateRequest projectRequest, string currentProjectAcronym)
        {
            if (string.IsNullOrWhiteSpace(projectRequest.ProjectAcronym))
                return false;

            if (projectRequest.ProjectAcronym.Equals(currentProjectAcronym))
                return false;

            return true;
        }

        #region private methods

        /// <summary>
        /// Assumes that the request has a name if desired changed.
        /// </summary>
        private static bool IsProjectNameUpdated(ProjectUpdateRequest projectRequest) 
        {
            return !string.IsNullOrWhiteSpace(projectRequest.Name);
        }

        /// <summary>
        /// Uses nested loops based on projectAcronym.
        /// </summary>
        private static Dictionary<ProjectDocument, ProjectNumbersDetails> CombineListsToDictionary(IEnumerable<ProjectDocument> projects, IEnumerable<ProjectNumbersDetails> projectsDetails) 
        {
            var listOfProjects = new List<ProjectDocument>(projects);
            var listOfProjectsDetails = new List<ProjectNumbersDetails>(projectsDetails);
            var projectsDictionary = new Dictionary<ProjectDocument, ProjectNumbersDetails>();

            foreach (var project in listOfProjects) 
            {
                foreach (var projectDetail in listOfProjectsDetails) 
                {
                    if (projectDetail.ProjectAcronym.Equals(project.ProjectAcronym)) 
                    {
                        projectsDictionary.Add(project, projectDetail);
                        // attempt to make it more efficient, might bring bugs in large numbers.
                        listOfProjectsDetails.Remove(projectDetail);
                        break;
                    }
                }
            }
            return projectsDictionary;
        }

        #endregion

        #region Project Numbers Details
        public static ProjectNumbersDetails MapToProjectNumbersDetails(ProjectsStoryNumberDocument projectsStoryNumber) 
        {
            return new ProjectNumbersDetails()
            {
                ProjectAcronym = projectsStoryNumber.ProjectAcronym,
                LatestStoryNumber = projectsStoryNumber.LatestStoryNumber,
                NumberOfActiveStories = projectsStoryNumber.NumberOfActiveStories,
                NumberOfStoriesCompleted = projectsStoryNumber.NumberOfStoriesCompleted
            };
        }

        public static IEnumerable<ProjectNumbersDetails> MapToProjectsNumbersDetails(IEnumerable<ProjectsStoryNumberDocument> projectsDetails)
        {
            var listOfProjectsNumbers = new List<ProjectNumbersDetails>();

            foreach (var projectNumber in projectsDetails)
            {
                listOfProjectsNumbers.Add(MapToProjectNumbersDetails(projectNumber));
            }

            return listOfProjectsNumbers;
        }


        public static ProjectNumbersDetails MapToEmptyProjectNumbersDetails()
        {
            return new EmptyProjectNumbersDetails();
        }

        #endregion
    }
}
