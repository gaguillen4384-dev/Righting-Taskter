using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    public class DomainUtilityDTOBuilder
    {
        private ProjectCreationRequest _newProjectRequest;
        private ProjectUpdateRequest _projectUpdateRequest;

        public DomainUtilityDTOBuilder()
        {
            _newProjectRequest = new ProjectCreationRequest();
            _projectUpdateRequest = new ProjectUpdateRequest();
        }

        public DomainUtilityDTOBuilder BuildNewProjectWithName(string name)
        {
            this._newProjectRequest.Name = name;
            return this;
        }
        public DomainUtilityDTOBuilder BuildNewProjectWithProjectAcronym(string acronym)
        {
            this._newProjectRequest.ProjectAcronym = acronym;
            return this;
        }

        public ProjectCreationRequest BuildNewProject()
        {
            return _newProjectRequest;
        }


        public DomainUtilityDTOBuilder BuildUpdateProjectWithName(string name)
        {
            this._projectUpdateRequest.Name = name;
            return this;
        }
        public DomainUtilityDTOBuilder BuildUpdateProjectWithProjectAcronym(string acronym)
        {
            this._projectUpdateRequest.ProjectAcronym = acronym;
            return this;
        }

        public ProjectUpdateRequest BuildUpdateProject()
        {
            return _projectUpdateRequest;
        }


        public ProjectCreationRequest BuildNewProjectRequest()
        {
            _newProjectRequest = new DomainUtilityDTOBuilder()
                    .BuildNewProjectWithProjectAcronym(NaturalValues.PrjAcronymToUse)
                    .BuildNewProjectWithName(NaturalValues.PrjName)
                    .BuildNewProject();

            return _newProjectRequest;
        }


        public ProjectUpdateRequest BuildUpdateProjectRequest()
        {
            _projectUpdateRequest = new DomainUtilityDTOBuilder()
                    .BuildUpdateProjectWithProjectAcronym(NaturalValues.PrjAcronymToUse)
                    .BuildUpdateProjectWithName(NaturalValues.PrjName)
                    .BuildUpdateProject();

            return _projectUpdateRequest;
        }
    }
}
