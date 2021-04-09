using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectUpdateBuilder : IProjectUpdateBuilder
    {
        public ProjectUpdateRequest _request;

        public ProjectUpdateBuilder() 
        {
            _request = new ProjectUpdateRequest();
        }

        public IProjectUpdateBuilder BuildProjectUpdateRequestWithAcronym(string acronym)
        {
            this._request.ProjectAcronym = acronym;
            return this;
        }

        public IProjectUpdateBuilder BuildProjectUpdateRequestWithName(string name)
        {
            this._request.Name = name;
            return this;
        }

        public ProjectUpdateRequest Build()
        {
            return _request;
        }
    }
}
