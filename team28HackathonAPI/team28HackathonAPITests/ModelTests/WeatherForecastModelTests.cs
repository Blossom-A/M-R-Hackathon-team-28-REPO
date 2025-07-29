using team28HackathonAPI;

namespace team28HackathonAPITests.ModelTests
{
    /// <summary>
    /// Unit tests for WeatherForecast model
    /// Tests business logic and property behavior without mocking (real logic testing)
    /// </summary>
    public class WeatherForecastModelTests
    {
        [Fact]
        public void TemperatureF_CalculatesCorrectlyFromCelsius()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 25 };

            // Act
            var fahrenheit = forecast.TemperatureF;

            // Assert
            var expected = 32 + (int)(25 / 0.5556);
            fahrenheit.Should().Be(expected);
        }

        [Theory]
        [InlineData(-20, -4)]   // Freezing cold
        [InlineData(0, 32)]     // Freezing point
        [InlineData(10, 50)]    // Cool
        [InlineData(20, 68)]    // Room temperature
        [InlineData(25, 77)]    // Warm
        [InlineData(30, 86)]    // Hot
        [InlineData(37, 98)]    // Body temperature
        [InlineData(50, 122)]   // Very hot
        public void TemperatureF_ConvertsVariousCelsiusValues(int celsius, int expectedFahrenheit)
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = celsius };

            // Act
            var fahrenheit = forecast.TemperatureF;

            // Assert
            fahrenheit.Should().Be(expectedFahrenheit);
        }

        [Fact]
        public void TemperatureF_IsReadOnlyProperty()
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act & Assert
            var property = typeof(WeatherForecast).GetProperty(nameof(WeatherForecast.TemperatureF));
            property.Should().NotBeNull();
            property.CanRead.Should().BeTrue();
            property.CanWrite.Should().BeFalse("TemperatureF should be a calculated read-only property");
        }

        [Fact]
        public void Date_CanBeSetAndRetrieved()
        {
            // Arrange
            var expectedDate = DateOnly.FromDateTime(DateTime.Now);
            var forecast = new WeatherForecast();

            // Act
            forecast.Date = expectedDate;

            // Assert
            forecast.Date.Should().Be(expectedDate);
        }

        [Fact]
        public void TemperatureC_CanBeSetAndRetrieved()
        {
            // Arrange
            var expectedTemperature = 25;
            var forecast = new WeatherForecast();

            // Act
            forecast.TemperatureC = expectedTemperature;

            // Assert
            forecast.TemperatureC.Should().Be(expectedTemperature);
        }

        [Fact]
        public void Summary_CanBeSetAndRetrieved()
        {
            // Arrange
            var expectedSummary = "Sunny";
            var forecast = new WeatherForecast();

            // Act
            forecast.Summary = expectedSummary;

            // Assert
            forecast.Summary.Should().Be(expectedSummary);
        }

        [Fact]
        public void Summary_CanBeNull()
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act
            forecast.Summary = null;

            // Assert
            forecast.Summary.Should().BeNull();
        }

        [Fact]
        public void DefaultConstructor_InitializesWithDefaultValues()
        {
            // Act
            var forecast = new WeatherForecast();

            // Assert
            forecast.Date.Should().Be(default(DateOnly));
            forecast.TemperatureC.Should().Be(0);
            forecast.Summary.Should().BeNull();
            forecast.TemperatureF.Should().Be(32); // 32 + (0 / 0.5556) = 32
        }

        [Fact]
        public void TemperatureF_RecalculatesWhenTemperatureCChanges()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 0 };
            var initialFahrenheit = forecast.TemperatureF;

            // Act
            forecast.TemperatureC = 100;
            var newFahrenheit = forecast.TemperatureF;

            // Assert
            initialFahrenheit.Should().Be(32);
            newFahrenheit.Should().Be(32 + (int)(100 / 0.5556));
            newFahrenheit.Should().NotBe(initialFahrenheit);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-100)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public void TemperatureC_AcceptsExtremeValues(int extremeTemperature)
        {
            // Arrange
            var forecast = new WeatherForecast();

            // Act
            forecast.TemperatureC = extremeTemperature;

            // Assert
            forecast.TemperatureC.Should().Be(extremeTemperature);
            // TemperatureF should still calculate without throwing
            var fahrenheit = forecast.TemperatureF;
            fahrenheit.Should().Be(32 + (int)(extremeTemperature / 0.5556));
        }

        [Fact]
        public void ObjectInitializer_WorksCorrectly()
        {
            // Arrange
            var expectedDate = DateOnly.FromDateTime(DateTime.Now);
            var expectedTemperature = 25;
            var expectedSummary = "Warm";

            // Act
            var forecast = new WeatherForecast
            {
                Date = expectedDate,
                TemperatureC = expectedTemperature,
                Summary = expectedSummary
            };

            // Assert
            forecast.Date.Should().Be(expectedDate);
            forecast.TemperatureC.Should().Be(expectedTemperature);
            forecast.Summary.Should().Be(expectedSummary);
            forecast.TemperatureF.Should().Be(32 + (int)(expectedTemperature / 0.5556));
        }

        [Fact]
        public void TemperatureConversionFormula_MatchesExpectedAlgorithm()
        {
            // This test verifies the exact formula used in the model
            // Formula: TemperatureF => 32 + (int)(TemperatureC / 0.5556);
            
            // Arrange & Act & Assert
            var testCases = new[]
            {
                new { Celsius = 0, Expected = 32 + (int)(0 / 0.5556) },
                new { Celsius = 100, Expected = 32 + (int)(100 / 0.5556) },
                new { Celsius = -40, Expected = 32 + (int)(-40 / 0.5556) },
                new { Celsius = 37, Expected = 32 + (int)(37 / 0.5556) }
            };

            foreach (var testCase in testCases)
            {
                var forecast = new WeatherForecast { TemperatureC = testCase.Celsius };
                forecast.TemperatureF.Should().Be(testCase.Expected, 
                    $"Temperature conversion for {testCase.Celsius}°C should match the exact formula");
            }
        }
    }
}
