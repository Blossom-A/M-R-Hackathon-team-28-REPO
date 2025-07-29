using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using team28HackathonAPI.DBContext;
using team28HackathonAPI.Models;

namespace team28HackathonAPITests
{
    /// <summary>
    /// Base class for unit tests providing common setup and utilities
    /// </summary>
    public abstract class TestBase : IDisposable
    {
        protected readonly Mock<ILogger<T>> CreateMockLogger<T>()
        {
            return new Mock<ILogger<T>>();
        }

        protected readonly Mock<HttpContext> CreateMockHttpContext()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();
            
            // Setup session mock
            mockSession.Setup(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()));
            mockSession.Setup(s => s.GetString(It.IsAny<string>())).Returns((string)null);
            
            mockHttpContext.Setup(c => c.Session).Returns(mockSession.Object);
            
            return mockHttpContext;
        }

        protected readonly Team28DbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Team28DbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new Team28DbContext(options);
        }

        protected readonly ControllerContext CreateControllerContext(HttpContext httpContext)
        {
            return new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        public virtual void Dispose()
        {
            // Override in derived classes if needed
        }
    }
}
