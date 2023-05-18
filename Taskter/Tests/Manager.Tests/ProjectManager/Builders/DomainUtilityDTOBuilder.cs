using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public class DomainUtilityDTOBuilder
    {
        private ProjectCreationRequest _newProjectRequest;

        public DomainUtilityDTOBuilder()
        {
            _newProjectRequest = new ProjectCreationRequest();
        }

        public DomainUtilityDTOBuilder BuildProjectWithName(string name)
        {
            this._newProjectRequest.Name = name;
            return this;
        }
        public DomainUtilityDTOBuilder BuildProjectWithProjectAcronym(string acronym)
        {
            this._newProjectRequest.ProjectAcronym = acronym;
            return this;
        }

        public ProjectCreationRequest BuildProject()
        {
            return _newProjectRequest;
        }


        public ProjectCreationRequest BuildNewProjectRequest()
        {
            _newProjectRequest = new DomainUtilityDTOBuilder()
                    .BuildProjectWithProjectAcronym(NaturalValues.PrjAcronymToUse)
                    .BuildProjectWithName(NaturalValues.PrjName)
                    .BuildProject();

            return _newProjectRequest;
        }
    }
}
