using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Taskter.Domain;

namespace ProjectManager
{
    //GETTO: Make it an Interface pattern if wanting to scale.
    public static class ManagerMapper
    {

        public static async Task<IEnumerable<ProjectResponse>> CombineProjectsAndMetadata(List<ProjectResponse> projectResponses, List<ProjectMetadataDetails> projectMetadataList)
        {
            var result = new List<ProjectResponse>();
            foreach (var projectResponse in projectResponses)
            {
                var localMetadata = projectMetadataList.FirstOrDefault(x => x.ProjectAcronym == projectResponse.ProjectAcronym);
                if (localMetadata is null)
                    continue;

                projectResponse.LatestStoryNumber = localMetadata.LatestStoryNumber;
                projectResponse.DateCreated = localMetadata.DateCreated;
                projectResponse.DateUpdated = localMetadata.DateUpdated;
                projectResponse.NumberOfActiveStories = localMetadata.NumberOfActiveStories;
                projectResponse.NumberOfCompletedStories = localMetadata.NumberOfStoriesCompleted;
                projectResponse.LastWorkedOn = localMetadata.LastWorkedOn;
                result.Add(projectResponse);
            }

            return result;
        }

        //GETTO: move to Manger.
        //private async Task<ProjectMetadataDetails> UpdateProjectAcronymReference(string updatedProjectAcronym, string projectAcronym, string projectId) 
        //{
        //    await UpdateStoryReferenceAcronym(updatedProjectAcronym, projectId);
        //    return await UpdateProjectMetadataAcronym(projectAcronym, updatedProjectAcronym);
        //}
    }
}
