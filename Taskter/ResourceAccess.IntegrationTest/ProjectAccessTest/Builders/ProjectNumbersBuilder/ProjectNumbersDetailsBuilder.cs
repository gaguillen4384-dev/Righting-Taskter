using Utilities.Taskter.Domain.Documents;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectNumbersDetailsBuilder : IProjectNumbersBuilder
    {
        // NEED TO INSTANTIATE THE BUILDER PROPERTIES BEFORE THEY GET USED
        public ProjectsStoryNumberDocument _details;

        public ProjectNumbersDetailsBuilder()
        {
            _details = new ProjectsStoryNumberDocument();
        }

        public IProjectNumbersBuilder BuildProjectDetailsWithAcronym(string acronym)
        {
            this._details.ProjectAcronym = acronym;
            return this;
        }

        public ProjectsStoryNumberDocument Build()
        {
            return _details;
        }
    }
}
