using ProjectAccessComponent;
using System;
using System.Collections.Generic;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectBuilder : IProjectBuilder
    {
        private List<ProjectDocument> _projects;
        private ProjectDocument _project;


        public IEnumerable<ProjectDocument> BuildManyProjects(int numberOfProjects)
        {
            for (int i = 0; i <= numberOfProjects; i++) 
            {
                _projects.Add(new ProjectBuilder()
                    .BuildProjectWithProjectAcronym($"PJT{i}")
                    .BuildProjectWithName($"Project{i}")
                    .Build());
            }

            return _projects;
        }

        public IProjectBuilder BuildProjectWithName(string name)
        {
            this._project.Name = name;
            return this;
        }

        public IProjectBuilder BuildProjectWithProjectAcronym(string projectAcronym)
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

        public ProjectDocument Build()
        {
            return _project;
        }
    }
}
