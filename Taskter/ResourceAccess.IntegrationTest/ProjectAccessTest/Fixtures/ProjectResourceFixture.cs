using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectAccessComponent;
using System;
using Utilities.Taskter.Domain;

namespace ResourceAccess.IntegrationTest.ProjectAccessTest
{
    public class ProjectResourceFixture : IDisposable
    {
        // TODO: Figure out the IOptions and where the sys grabbing it from.
        
        public ServiceProvider ServiceProvider { get; private set; }

        public ProjectResourceFixture()
        {
            // Initialize stuff
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            // this is by Microsoft.Extensions.Options.ConfigurationExtensions which allows strongtype 
            // Supposed to fill in IOptions
            services.Configure<ProjectResourceConfig>(configuration.GetSection("ProjectResourceConfig"));

            services.AddTransient<IProjectAccess, ProjectAccess>();
            services.AddTransient<IProjectBuilder, ProjectBuilder>();
            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            // delete DB 
        }
    }
}
