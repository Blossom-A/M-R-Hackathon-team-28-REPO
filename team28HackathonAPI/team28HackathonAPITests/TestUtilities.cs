using team28HackathonAPI.Controllers;

namespace team28HackathonAPITests
{
    /// <summary>
    /// Utility class containing test data and helper methods
    /// </summary>
    public static class TestUtilities
    {
        /// <summary>
        /// Valid login request for testing
        /// </summary>
        public static LoginRequest ValidLoginRequest => new LoginRequest
        {
            Username = "testuser",
            Password = "password123"
        };

        /// <summary>
        /// Invalid login request with wrong username
        /// </summary>
        public static LoginRequest InvalidUsernameLoginRequest => new LoginRequest
        {
            Username = "wronguser",
            Password = "password123"
        };

        /// <summary>
        /// Invalid login request with wrong password
        /// </summary>
        public static LoginRequest InvalidPasswordLoginRequest => new LoginRequest
        {
            Username = "testuser",
            Password = "wrongpassword"
        };

        /// <summary>
        /// Login request with null username
        /// </summary>
        public static LoginRequest NullUsernameLoginRequest => new LoginRequest
        {
            Username = null,
            Password = "password123"
        };

        /// <summary>
        /// Login request with null password
        /// </summary>
        public static LoginRequest NullPasswordLoginRequest => new LoginRequest
        {
            Username = "testuser",
            Password = null
        };

        /// <summary>
        /// Login request with empty strings
        /// </summary>
        public static LoginRequest EmptyLoginRequest => new LoginRequest
        {
            Username = "",
            Password = ""
        };

        /// <summary>
        /// Expected weather forecast summaries
        /// </summary>
        public static readonly string[] ExpectedWeatherSummaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Validates temperature conversion from Celsius to Fahrenheit
        /// </summary>
        /// <param name="celsius">Temperature in Celsius</param>
        /// <returns>Expected temperature in Fahrenheit</returns>
        public static int CalculateExpectedFahrenheit(int celsius)
        {
            return 32 + (int)(celsius / 0.5556);
        }
    }
}
