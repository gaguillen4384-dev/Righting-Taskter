using System.Collections.Generic;
using Utilities.Taskter.Domain;

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
        public static ProjectResponse MapToProjectResponse(ProjectDocument project) 
        {
            return new ProjectResponse()
            {
                // TODO: finish this mapping.
            };
        }

        public static IEnumerable<ProjectResponse> MapToProjectsResponse(IEnumerable<ProjectDocument> projects) 
        {
            var listOfProjects = new List<ProjectResponse>();

            foreach (var project in projects) 
            {
                listOfProjects.Add(MapToProjectResponse(project));
            }

            return listOfProjects;
        }


        public static ProjectDocument MapToProjectDocumentFromRequest(ProjectCreationRequest projectRequest) 
        {
            return new ProjectDocument()
            {
                // TODO: finish this mapping.
            };
        }
    }
}
