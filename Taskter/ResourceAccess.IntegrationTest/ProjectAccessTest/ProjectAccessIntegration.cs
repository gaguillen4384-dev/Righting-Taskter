using Microsoft.Extensions.DependencyInjection;
using ProjectAccessComponent;
using Xunit;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectAccessIntegration : IClassFixture<ProjectResourceFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IProjectAccess _projectAccess;
        private readonly IProjectBuilder _projectBuilder;

        public ProjectAccessIntegration(ProjectResourceFixture fixture) 
        {
            _serviceProvider = fixture.ServiceProvider;
            // System components
            _projectAccess = _serviceProvider.GetService<IProjectAccess>();

            // Test components
            _projectBuilder = _serviceProvider.GetService<IProjectBuilder>();
        }

        [Fact]
        public void ProjectAccess_GetAllAvailableProjects()
        {
            // Arrange


            // Act
            var result = _projectAccess.OpenProjects();

            // Assert - descriptive

            // Teardown?
        }
    }
}
