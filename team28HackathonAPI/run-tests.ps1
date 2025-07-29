#!/usr/bin/env pwsh

Write-Host "========================================" -ForegroundColor Cyan
Write-Host " Team 28 Backend Test Runner" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Check if dotnet is available
try {
    $dotnetVersion = dotnet --version 2>$null
    Write-Host "✅ .NET SDK found: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ ERROR: .NET SDK not found in PATH" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please install .NET 8.0 SDK from:" -ForegroundColor Yellow
    Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Or add .NET to your PATH if already installed." -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "🔄 Restoring NuGet packages..." -ForegroundColor Yellow
try {
    dotnet restore team28HackathonAPITests/team28HackathonAPITests.csproj
    Write-Host "✅ Packages restored successfully" -ForegroundColor Green
} catch {
    Write-Host "❌ ERROR: Failed to restore packages" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "🔨 Building test project..." -ForegroundColor Yellow
try {
    dotnet build team28HackathonAPITests/team28HackathonAPITests.csproj --no-restore
    Write-Host "✅ Build completed successfully" -ForegroundColor Green
} catch {
    Write-Host "❌ ERROR: Failed to build test project" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "🧪 Running all tests..." -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan

# Run tests with detailed output
dotnet test team28HackathonAPITests/team28HackathonAPITests.csproj --no-build --verbosity normal --logger "console;verbosity=detailed"

$testResult = $LASTEXITCODE

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
if ($testResult -eq 0) {
    Write-Host "✅ All tests completed successfully!" -ForegroundColor Green
} else {
    Write-Host "❌ Some tests failed. Check the output above." -ForegroundColor Red
}
Write-Host "========================================" -ForegroundColor Cyan

# Additional test commands you can run:
Write-Host ""
Write-Host "📋 Additional test commands you can run:" -ForegroundColor Cyan
Write-Host "  • Run specific test class:" -ForegroundColor Gray
Write-Host "    dotnet test --filter 'AuthControllerTests'" -ForegroundColor Gray
Write-Host "  • Run with code coverage:" -ForegroundColor Gray
Write-Host "    dotnet test --collect:'XPlat Code Coverage'" -ForegroundColor Gray
Write-Host "  • Run integration tests only:" -ForegroundColor Gray
Write-Host "    dotnet test --filter 'Category=Integration'" -ForegroundColor Gray

Read-Host "Press Enter to exit"
