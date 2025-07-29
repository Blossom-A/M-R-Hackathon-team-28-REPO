using Microsoft.Extensions.Logging;
using team28HackathonAPI;
using team28HackathonAPI.Controllers;

namespace team28HackathonAPITests.ControllerTests
{
    /// <summary>
    /// Unit tests for WeatherForecastController with mocked dependencies
    /// Tests weather forecast generation logic without external dependencies
    /// </summary>
    public class WeatherForecastControllerTests : TestBase
    {
        private readonly WeatherForecastController _controller;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;

        public WeatherForecastControllerTests()
        {
            _mockLogger = CreateMockLogger<WeatherForecastController>();
            _controller = new WeatherForecastController(_mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsExactlyFiveForecasts()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        [Fact]
        public void Get_ReturnsWeatherForecastObjects()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().AllBeOfType<WeatherForecast>();
        }

        [Fact]
        public void Get_AllForecastsHaveValidDates()
        {
            // Act
            var result = _controller.Get().ToList();

            // Assert
            result.Should().NotBeNull();
            
            for (int i = 0; i < result.Count; i++)
            {
                var expectedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i + 1));
                result[i].Date.Should().Be(expectedDate);
            }
        }

        [Fact]
        public void Get_AllForecastsHaveTemperatureInValidRange()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().OnlyContain(f => f.TemperatureC >= -20 && f.TemperatureC < 55);
        }

        [Fact]
        public void Get_AllForecastsHaveValidSummaries()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().OnlyContain(f => TestUtilities.ExpectedWeatherSummaries.Contains(f.Summary));
        }

        [Fact]
        public void Get_TemperatureFahrenheitCalculatedCorrectly()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            
            foreach (var forecast in result)
            {
                var expectedFahrenheit = TestUtilities.CalculateExpectedFahrenheit(forecast.TemperatureC);
                forecast.TemperatureF.Should().Be(expectedFahrenheit);
            }
        }

        [Fact]
        public void Get_ReturnsConsistentDataStructure()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            
            foreach (var forecast in result)
            {
                forecast.Date.Should().NotBe(default(DateOnly));
                forecast.Summary.Should().NotBeNullOrEmpty();
                // TemperatureC can be any valid integer in range
                // TemperatureF should be calculated property
            }
        }

        [Fact]
        public void Get_MultipleCallsReturnDifferentData()
        {
            // Act
            var result1 = _controller.Get().ToList();
            var result2 = _controller.Get().ToList();

            // Assert
            result1.Should().NotBeNull();
            result2.Should().NotBeNull();
            result1.Should().HaveCount(5);
            result2.Should().HaveCount(5);

            // Due to randomization, at least some values should be different
            // (This test might occasionally fail due to random chance, but very unlikely)
            var temperaturesDifferent = result1.Zip(result2, (f1, f2) => f1.TemperatureC != f2.TemperatureC).Any(x => x);
            var summariesDifferent = result1.Zip(result2, (f1, f2) => f1.Summary != f2.Summary).Any(x => x);
            
            (temperaturesDifferent || summariesDifferent).Should().BeTrue("Random data should vary between calls");
        }

        [Fact]
        public void Get_DoesNotLogErrors()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().NotBeNull();
            
            // Verify no error-level logging occurred
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }

        [Theory]
        [InlineData(-20, 32 + (int)(-20 / 0.5556))]
        [InlineData(0, 32)]
        [InlineData(25, 32 + (int)(25 / 0.5556))]
        [InlineData(54, 32 + (int)(54 / 0.5556))]
        public void Get_TemperatureConversionIsAccurate(int celsius, int expectedFahrenheit)
        {
            // This test verifies the temperature conversion logic by checking
            // if any forecast with the given celsius temperature has the correct fahrenheit value
            
            // Act - Get multiple results to increase chance of hitting the target temperature
            var allResults = new List<WeatherForecast>();
            for (int i = 0; i < 50; i++) // Multiple calls to get various temperatures
            {
                allResults.AddRange(_controller.Get());
            }

            // Assert
            var forecastsWithTargetTemp = allResults.Where(f => f.TemperatureC == celsius).ToList();
            
            if (forecastsWithTargetTemp.Any())
            {
                forecastsWithTargetTemp.Should().OnlyContain(f => f.TemperatureF == expectedFahrenheit);
            }
            // If no forecasts with target temperature were generated, test passes (random nature)
        }

        public override void Dispose()
        {
            _controller?.Dispose();
            base.Dispose();
        }
    }
}
