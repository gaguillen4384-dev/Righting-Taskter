using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Taskter.Domain;

namespace Manager.Tests.ProjectManager
{
    //GETTO: Split this up for the different responses/request. 
    public  class DomainUtilityBuilder
    {
        private List<ProjectResponse> _projects;
        private ProjectResponse _project;

        public DomainUtilityBuilder()
        {
            _project = new ProjectResponse();
            _projects = new List<ProjectResponse>();
        }

        public DomainUtilityBuilder BuildProjectWithName(string name)
        {
            this._project.Name = name;
            return this;
        }
        public DomainUtilityBuilder BuildProjectWithProjectAcronym(string name)
        {
            this._project.ProjectAcronym = name;
            return this;
        }
        public DomainUtilityBuilder BuildProjectWithNumberOfActiveStories(int numberOfActiveStories)
        {
            this._project.NumberOfActiveStories = numberOfActiveStories;
            return this;
        }
        public DomainUtilityBuilder BuildProjectWithNumberOfCompletedStories(int numberOfCompletedStories)
        {
            this._project.NumberOfCompletedStories = numberOfCompletedStories;
            return this;
        }

        public IEnumerable<ProjectResponse> BuildMultipleProjects(int numberOfProjects)
        {
            for (int i = 0; i < numberOfProjects; i++)
            {
                var random = new Random();
                _projects.Add(new DomainUtilityBuilder()
                    .BuildProjectWithProjectAcronym($"{NaturalValues.ProjectAcronymToUse}{i}")
                    .BuildProjectWithName($"{NaturalValues.ProjectNameToUse}{i}")
                    .BuildProjectWithNumberOfCompletedStories(random.Next(i+ random.Next(i)))
                    .BuildProjectWithNumberOfActiveStories(random.Next(i + random.Next(i)))
                    .BuildProject());
            }

            return _projects;
        }

        public ProjectResponse BuildSingleProject()
        {
            int i = 2;
            var random = new Random();
            _project = new DomainUtilityBuilder()
                .BuildProjectWithProjectAcronym(NaturalValues.ProjectAcronymToUse)
                .BuildProjectWithName($"{NaturalValues.ProjectNameToUse}")
                .BuildProjectWithNumberOfCompletedStories(random.Next(i + random.Next(i)))
                .BuildProjectWithNumberOfActiveStories(random.Next(i + random.Next(i)))
                .BuildProject();

            return _project;
        }

        public ProjectResponse BuildProject() 
        {
            return _project;
        }

    }
}
