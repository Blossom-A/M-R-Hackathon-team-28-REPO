# Team 28 Backend Testing Suite

This directory contains comprehensive unit and integration tests for the Team 28 Hackathon API project.

## 🧪 Test Structure

### Unit Tests (with Mocking)
- **`ControllerTests/UserControllerTests.tests.cs`** - AuthController tests with mocked HttpContext/Session
- **`ControllerTests/WeatherForecastControllerTests.cs`** - WeatherForecastController tests with mocked ILogger
- **`ModelTests/WeatherForecastModelTests.cs`** - WeatherForecast model business logic tests

### Integration Tests
- **`IntegrationTests/ApiIntegrationTests.cs`** - End-to-end API tests with TestServer
- **`IntegrationTests/IntegrationTestBase.cs`** - Base class for integration testing

### Test Infrastructure
- **`TestBase.cs`** - Base class for unit tests with common mocking utilities
- **`TestUtilities.cs`** - Shared test data and helper methods

## 🚀 Running Tests

### Prerequisites
- .NET 8.0 SDK installed
- Visual Studio 2022 or VS Code with C# extension

### Method 1: Using Test Runner Scripts
```bash
# Windows Command Prompt
run-tests.bat

# PowerShell (Windows/Linux/Mac)
./run-tests.ps1
```

### Method 2: .NET CLI Commands
```bash
# Navigate to API directory
cd team28HackathonAPI

# Run all tests
dotnet test team28HackathonAPITests/team28HackathonAPITests.csproj

# Run with detailed output
dotnet test team28HackathonAPITests/team28HackathonAPITests.csproj --verbosity normal

# Run specific test class
dotnet test --filter "AuthControllerTests"
dotnet test --filter "WeatherForecastControllerTests"
dotnet test --filter "ApiIntegrationTests"

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Method 3: Visual Studio
1. Open `team28HackathonAPI.sln`
2. Go to **Test** → **Run All Tests**
3. Use **Test Explorer** window for detailed results

### Method 4: VS Code
1. Install C# extension
2. Open Command Palette (`Ctrl+Shift+P`)
3. Run **"Test: Run All Tests"**

## 📊 Test Coverage

### AuthController Tests
- ✅ Valid login credentials
- ✅ Invalid username/password combinations
- ✅ Null and empty input handling
- ✅ Session management verification
- ✅ Case sensitivity testing

### WeatherForecastController Tests
- ✅ Returns exactly 5 forecasts
- ✅ Valid temperature ranges (-20°C to 54°C)
- ✅ Correct weather summaries
- ✅ Temperature conversion accuracy
- ✅ Data randomization verification
- ✅ Logger interaction testing

### WeatherForecast Model Tests
- ✅ Temperature conversion formula (C° to F°)
- ✅ Property getters/setters
- ✅ Edge case handling
- ✅ Read-only property validation

### Integration Tests
- ✅ HTTP endpoint accessibility
- ✅ JSON serialization/deserialization
- ✅ CORS header validation
- ✅ Concurrent request handling
- ✅ Error response testing

## 🎯 Mocking Strategy

### What We Mock
- **HttpContext & Session** - For authentication testing without real sessions
- **ILogger** - For controller testing without actual logging
- **Database Context** - Using in-memory database for integration tests

### What We Don't Mock (Real Logic Testing)
- **Business logic** - Temperature conversion algorithms
- **Model properties** - Data validation and behavior
- **HTTP responses** - Actual controller return values
- **JSON serialization** - Real API response structure

## 🔧 Test Dependencies

```xml
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
<PackageReference Include="FluentAssertions" Version="6.12.0" />
<PackageReference Include="xunit" Version="2.5.3" />
```

## 📈 Expected Results

When all tests pass, you should see:
- **AuthController**: 8 tests passing
- **WeatherForecastController**: 10 tests passing  
- **WeatherForecast Model**: 12 tests passing
- **Integration Tests**: 12 tests passing

**Total: ~42 tests** covering authentication, weather API, business logic, and end-to-end scenarios.

## 🐛 Troubleshooting

### Common Issues
1. **"dotnet command not found"** - Install .NET 8.0 SDK
2. **Package restore fails** - Check internet connection and NuGet sources
3. **Build errors** - Ensure main API project builds successfully first
4. **Test failures** - Check if main API endpoints are working correctly

### Debug Tips
- Use `--verbosity detailed` for more test output
- Run individual test classes to isolate issues
- Check Test Explorer in Visual Studio for detailed failure messages
- Verify mock setups match actual controller dependencies

## 📝 Adding New Tests

1. **Unit Tests**: Extend existing test classes or create new ones in appropriate folders
2. **Integration Tests**: Add new test methods to `ApiIntegrationTests.cs`
3. **Test Data**: Add new test scenarios to `TestUtilities.cs`
4. **Mocks**: Use `TestBase.cs` helper methods for consistent mocking

Follow the established patterns for consistency and maintainability!
