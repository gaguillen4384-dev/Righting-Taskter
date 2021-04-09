using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public interface IProjectUpdateBuilder
    {
        IProjectUpdateBuilder BuildProjectUpdateRequestWithAcronym(string acronym);

        IProjectUpdateBuilder BuildProjectUpdateRequestWithName(string name);

        ProjectUpdateRequest Build();
    }
}
