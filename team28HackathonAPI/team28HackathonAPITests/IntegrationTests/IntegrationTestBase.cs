using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using team28HackathonAPI.DBContext;

namespace team28HackathonAPITests.IntegrationTests
{
    /// <summary>
    /// Base class for integration tests using TestServer
    /// Provides end-to-end API testing without external dependencies
    /// </summary>
    public abstract class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        protected readonly WebApplicationFactory<Program> _factory;
        protected readonly HttpClient _client;

        protected IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the real database context
                    services.RemoveAll(typeof(DbContextOptions<Team28DbContext>));
                    services.RemoveAll(typeof(Team28DbContext));

                    // Add in-memory database for testing
                    services.AddDbContext<Team28DbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase_" + Guid.NewGuid().ToString());
                    });

                    // Build service provider and ensure database is created
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<Team28DbContext>();
                    context.Database.EnsureCreated();
                });

                builder.UseEnvironment("Testing");
            });

            _client = _factory.CreateClient();
        }

        /// <summary>
        /// Creates a new HTTP client for testing
        /// </summary>
        protected HttpClient CreateClient()
        {
            return _factory.CreateClient();
        }

        /// <summary>
        /// Gets a scoped service from the test server
        /// </summary>
        protected T GetService<T>() where T : class
        {
            using var scope = _factory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Seeds the test database with initial data
        /// </summary>
        protected void SeedDatabase(Action<Team28DbContext> seedAction)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Team28DbContext>();
            seedAction(context);
            context.SaveChanges();
        }

        /// <summary>
        /// Clears all data from the test database
        /// </summary>
        protected void ClearDatabase()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Team28DbContext>();
            
            // Clear all entities if any exist in the future
            // For now, the context doesn't have any DbSets defined
            context.SaveChanges();
        }

        public virtual void Dispose()
        {
            _client?.Dispose();
        }
    }
}
