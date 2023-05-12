using System;
using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    //GETTO: Split this up for the different responses/request. 
    public  class DomainUtilityProjectBuilder
    {
        private List<ProjectResponse> _projects;
        private ProjectResponse _project;

        public DomainUtilityProjectBuilder()
        {
            _project = new ProjectResponse();
            _projects = new List<ProjectResponse>();
        }

        #region Builder methods
        #region Project Building
        public DomainUtilityProjectBuilder BuildProjectWithName(string name)
        {
            this._project.Name = name;
            return this;
        }
        public DomainUtilityProjectBuilder BuildProjectWithProjectAcronym(string acronym)
        {
            this._project.ProjectAcronym = acronym;
            return this;
        }
        public DomainUtilityProjectBuilder BuildProjectWithNumberOfActiveStories(int numberOfActiveStories)
        {
            this._project.NumberOfActiveStories = numberOfActiveStories;
            return this;
        }
        public DomainUtilityProjectBuilder BuildProjectWithNumberOfCompletedStories(int numberOfCompletedStories)
        {
            this._project.NumberOfCompletedStories = numberOfCompletedStories;
            return this;
        }
        public DomainUtilityProjectBuilder BuildProjectWithLatestStoryNumber(int latestStoryNumber)
        {
            this._project.NumberOfActiveStories = latestStoryNumber;
            return this;
        }
        public ProjectResponse BuildProject()
        {
            return _project;
        }
        #endregion
        #endregion

        #region Setup Methods

        public IEnumerable<ProjectResponse> BuildMultipleProjects(int numberOfProjects, string optionalAcronym = null)
        {

            for (int i = 0; i < numberOfProjects; i++)
            {
                var random = new Random();
                _projects.Add(new DomainUtilityProjectBuilder()
                    .BuildProjectWithProjectAcronym(string.IsNullOrWhiteSpace(optionalAcronym) ? $"PJT{i}" : optionalAcronym)
                    .BuildProjectWithName($"Project{i}")
                    .BuildProjectWithNumberOfCompletedStories(random.Next(i+ random.Next(i)))
                    .BuildProjectWithNumberOfActiveStories(random.Next(i + random.Next(i)))
                    .BuildProjectWithLatestStoryNumber(i)
                    .BuildProject());
            }

            return _projects;
        }



        #endregion
    }
}
