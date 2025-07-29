@echo off
echo ========================================
echo  Push Backend Testing Implementation
echo ========================================
echo.

echo Current directory: %CD%
echo.

echo Checking git status...
git status
echo.

echo Adding all new test files...
git add team28HackathonAPI/team28HackathonAPITests/
git add team28HackathonAPI/run-tests.bat
git add team28HackathonAPI/run-tests.ps1
git add README.md
echo.

echo Checking what will be committed...
git status
echo.

echo Committing changes...
git commit -m "feat: Add comprehensive backend testing suite

- Add 42+ unit and integration tests for backend API
- Implement proper mocking strategy (HttpContext, Session, ILogger)
- Add AuthController tests with session management validation
- Add WeatherForecastController tests with mocked dependencies
- Add WeatherForecast model tests for business logic
- Add integration tests with TestServer and in-memory database
- Create test utilities and base classes for reusability
- Add test runner scripts (batch and PowerShell)
- Update README with comprehensive testing documentation
- Include detailed testing README in test project

Test Coverage:
- AuthController: 8 tests (login validation, edge cases)
- WeatherForecastController: 10 tests (data structure, mocking)
- WeatherForecast Model: 12 tests (temperature conversion logic)
- Integration Tests: 12 tests (end-to-end HTTP testing)

Technologies: xUnit, Moq, FluentAssertions, ASP.NET Core Testing"

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo No changes to commit or commit failed.
    echo Checking current status...
    git status
    pause
    exit /b 1
)

echo.
echo Pushing to remote repository...
echo Creating and pushing 'israel' branch...
git push -u origin israel

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo Push failed. This might be because:
    echo 1. You need to authenticate with GitHub
    echo 2. The remote repository is not accessible
    echo 3. Network connectivity issues
    echo.
    echo You can try pushing manually with:
    echo git push -u origin israel
    echo.
    pause
    exit /b 1
)

echo.
echo ========================================
echo ✅ SUCCESS! Backend tests pushed to GitHub
echo ========================================
echo.
echo Your changes have been pushed to the 'israel' branch.
echo You can now:
echo 1. Create a Pull Request on GitHub
echo 2. Merge with the main branch
echo 3. Share the testing implementation with your team
echo.
echo Repository: https://github.com/Blossom-A/M-R-Hackathon-team-28-REPO
echo Branch: israel
echo.
pause
