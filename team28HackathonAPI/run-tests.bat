@echo off
echo ========================================
echo  Team 28 Backend Test Runner
echo ========================================
echo.

REM Check if dotnet is available
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: .NET SDK not found in PATH
    echo.
    echo Please install .NET 8.0 SDK from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    echo Or add .NET to your PATH if already installed.
    pause
    exit /b 1
)

echo Checking .NET version...
dotnet --version
echo.

echo Restoring NuGet packages...
dotnet restore team28HackathonAPITests/team28HackathonAPITests.csproj
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)
echo.

echo Building test project...
dotnet build team28HackathonAPITests/team28HackathonAPITests.csproj --no-restore
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to build test project
    pause
    exit /b 1
)
echo.

echo Running all tests...
echo ========================================
dotnet test team28HackathonAPITests/team28HackathonAPITests.csproj --no-build --verbosity normal --logger "console;verbosity=detailed"

echo.
echo ========================================
echo Test run completed!
echo ========================================
pause
