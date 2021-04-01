using System;
using System.Collections.Generic;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectBuilder : IProjectCreationBuilder
    {
        private List<ProjectCreationRequest> _projects;
        private ProjectCreationRequest _project;

        // NEED TO INSTANTIATE THE BUILDER PROPERTIES BEFORE THEY GET USED
        public ProjectBuilder() 
        {
            _project = new ProjectCreationRequest();
            _projects = new List<ProjectCreationRequest>();
        }


        public IEnumerable<ProjectCreationRequest> BuildManyProjects(int numberOfProjects)
        {
            for (int i = 0; i < numberOfProjects; i++) 
            {
                _projects.Add(new ProjectBuilder()
                    .BuildProjectWithProjectAcronym($"PJT{i}")
                    .BuildProjectWithName($"Project{i}")
                    .Build());
            }

            return _projects;
        }

        public IProjectCreationBuilder BuildProjectWithName(string name)
        {
            this._project.Name = name;
            return this;
        }

        public IProjectCreationBuilder BuildProjectWithProjectAcronym(string projectAcronym)
        {
            this._project.ProjectAcronym = projectAcronym;
            return this;
        }

        // TODO: Their own builder.
        public void BuildProjectWithNumberOfActiveStories(int numberOfActiveStories)
        {
            throw new NotImplementedException();
        }

        // TODO: Their own builder.
        public void BuildProjectWithNumberOfCompletedStories(int numberOfCompletedStories)
        {
            throw new NotImplementedException();
        }

        public ProjectCreationRequest Build()
        {
            return _project;
        }
    }
}
