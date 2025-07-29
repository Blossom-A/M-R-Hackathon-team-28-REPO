using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team28HackathonAPI.Controllers;

namespace team28HackathonAPITests.ControllerTests
{
    /// <summary>
    /// Unit tests for AuthController with mocked dependencies
    /// Tests authentication logic without real session or database dependencies
    /// </summary>
    public class AuthControllerTests : TestBase
    {
        private readonly AuthController _controller;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ISession> _mockSession;

        public AuthControllerTests()
        {
            _mockHttpContext = CreateMockHttpContext();
            _mockSession = new Mock<ISession>();

            // Setup session mock
            _mockHttpContext.Setup(c => c.Session).Returns(_mockSession.Object);

            _controller = new AuthController();
            _controller.ControllerContext = CreateControllerContext(_mockHttpContext.Object);
        }

        [Fact]
        public void Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginRequest = TestUtilities.ValidLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().NotBeNull();

            // Verify session was set
            _mockSession.Verify(s => s.SetString("User", loginRequest.Username), Times.Once);
        }

        [Fact]
        public void Login_WithValidCredentials_ReturnsCorrectMessage()
        {
            // Arrange
            var loginRequest = TestUtilities.ValidLoginRequest;

            // Act
            var result = _controller.Login(loginRequest) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            var responseValue = result.Value;
            responseValue.Should().NotBeNull();

            // Check if the response contains the expected message
            var message = responseValue.GetType().GetProperty("Message")?.GetValue(responseValue);
            message.Should().Be("Login successful");
        }

        [Fact]
        public void Login_WithInvalidUsername_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.InvalidUsernameLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().Be("Invalid username or password.");

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_WithInvalidPassword_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.InvalidPasswordLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Value.Should().Be("Invalid username or password.");

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_WithNullUsername_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.NullUsernameLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_WithNullPassword_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.NullPasswordLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_WithEmptyCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.EmptyLoginRequest;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Login_WithNullRequest_ThrowsException()
        {
            // Arrange
            LoginRequest loginRequest = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _controller.Login(loginRequest));

            // Verify session was not set
            _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("testuser", "password123", true)]
        [InlineData("TESTUSER", "password123", false)] // Case sensitive username
        [InlineData("testuser", "PASSWORD123", false)] // Case sensitive password
        [InlineData("test", "password123", false)]     // Partial username
        [InlineData("testuser", "pass", false)]        // Partial password
        public void Login_WithVariousCredentials_ReturnsExpectedResult(string username, string password, bool shouldSucceed)
        {
            // Arrange
            var loginRequest = new LoginRequest { Username = username, Password = password };

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            if (shouldSucceed)
            {
                result.Should().BeOfType<OkObjectResult>();
                _mockSession.Verify(s => s.SetString("User", username), Times.Once);
            }
            else
            {
                result.Should().BeOfType<UnauthorizedObjectResult>();
                _mockSession.Verify(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            }
        }

        public override void Dispose()
        {
            _controller?.Dispose();
            base.Dispose();
        }
    }
}
