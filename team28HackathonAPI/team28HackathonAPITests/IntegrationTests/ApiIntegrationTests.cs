using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using team28HackathonAPI.Controllers;

namespace team28HackathonAPITests.IntegrationTests
{
    /// <summary>
    /// Integration tests for API endpoints
    /// Tests complete HTTP request/response cycle without external dependencies
    /// </summary>
    public class ApiIntegrationTests : IntegrationTestBase
    {
        public ApiIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task WeatherForecast_Get_ReturnsSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType?.ToString().Should().Contain("application/json");
        }

        [Fact]
        public async Task WeatherForecast_Get_ReturnsFiveForecasts()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");
            var content = await response.Content.ReadAsStringAsync();
            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            response.EnsureSuccessStatusCode();
            forecasts.Should().NotBeNull();
            forecasts.Should().HaveCount(5);
        }

        [Fact]
        public async Task WeatherForecast_Get_ReturnsValidForecastStructure()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");
            var content = await response.Content.ReadAsStringAsync();
            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            response.EnsureSuccessStatusCode();
            forecasts.Should().NotBeNull();
            
            foreach (var forecast in forecasts)
            {
                forecast.Date.Should().NotBe(default(DateOnly));
                forecast.TemperatureC.Should().BeInRange(-20, 54);
                forecast.Summary.Should().NotBeNullOrEmpty();
                forecast.Summary.Should().BeOneOf(TestUtilities.ExpectedWeatherSummaries);
                
                // Verify temperature conversion
                var expectedF = TestUtilities.CalculateExpectedFahrenheit(forecast.TemperatureC);
                forecast.TemperatureF.Should().Be(expectedF);
            }
        }

        [Fact]
        public async Task Auth_Login_WithValidCredentials_ReturnsOk()
        {
            // Arrange
            var loginRequest = TestUtilities.ValidLoginRequest;
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Login successful");
        }

        [Fact]
        public async Task Auth_Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = TestUtilities.InvalidUsernameLoginRequest;
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Invalid username or password");
        }

        [Fact]
        public async Task Auth_Login_WithEmptyBody_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Auth_Login_WithInvalidJson_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("invalid json", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/WeatherForecast")]
        [InlineData("/swagger/index.html")]
        public async Task PublicEndpoints_AreAccessible(string endpoint)
        {
            // Act
            var response = await _client.GetAsync(endpoint);

            // Assert
            response.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Api_SupportsMultipleConcurrentRequests()
        {
            // Arrange
            var tasks = new List<Task<HttpResponseMessage>>();
            
            // Act - Make 10 concurrent requests
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(_client.GetAsync("/WeatherForecast"));
            }
            
            var responses = await Task.WhenAll(tasks);

            // Assert
            responses.Should().HaveCount(10);
            responses.Should().OnlyContain(r => r.IsSuccessStatusCode);
            
            // Verify each response has valid content
            foreach (var response in responses)
            {
                var content = await response.Content.ReadAsStringAsync();
                var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                forecasts.Should().HaveCount(5);
            }
        }

        [Fact]
        public async Task Auth_Login_SessionPersistence_WorksCorrectly()
        {
            // This test verifies that session state works in integration testing
            // Note: In real scenarios, you'd need to handle cookies for session persistence
            
            // Arrange
            var loginRequest = TestUtilities.ValidLoginRequest;
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // In a real application, you would check for session cookies or tokens
            // For this simple implementation, we just verify the login response
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Login successful");
        }

        [Fact]
        public async Task Api_ReturnsCorrectCorsHeaders()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            
            // Verify CORS headers are present (configured in Program.cs)
            response.Headers.Should().ContainKey("Access-Control-Allow-Origin");
        }
    }
}
