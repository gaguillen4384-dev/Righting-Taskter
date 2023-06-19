using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public class RequestUtilityBuilder
    {
        private ProjectCreationRequest _project;

        public RequestUtilityBuilder()
        {
            _project = new ProjectCreationRequest();
        }

        public RequestUtilityBuilder BuildProjectRequestWithName(string name) 
        {
            this._project.Name = name;
            return this;
        }

        public RequestUtilityBuilder BuildProjectRequestWithAcronym(string acronym)
        {
            this._project.ProjectAcronym = acronym;
            return this;
        }

        public ProjectCreationRequest BuildProjectRequest() 
        {
            return _project;
        }

        public ProjectCreationRequest BuildCreateProjectRequest() 
        {
            _project = new RequestUtilityBuilder()
               .BuildProjectRequestWithName(NaturalValues.ProjectNameToUse)
               .BuildProjectRequestWithAcronym(NaturalValues.ProjectAcronymToUse)
               .BuildProjectRequest();
            return _project;
        }
    }
}
