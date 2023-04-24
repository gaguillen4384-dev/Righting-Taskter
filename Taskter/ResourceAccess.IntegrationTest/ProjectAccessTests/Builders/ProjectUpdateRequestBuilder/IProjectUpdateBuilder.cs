using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTests
{
    public interface IProjectUpdateBuilder
    {
        IProjectUpdateBuilder BuildProjectUpdateRequestWithAcronym(string acronym);

        IProjectUpdateBuilder BuildProjectUpdateRequestWithName(string name);

        ProjectUpdateRequest Build();
    }
}
