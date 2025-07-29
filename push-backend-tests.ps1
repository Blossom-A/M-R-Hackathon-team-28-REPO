#!/usr/bin/env pwsh

Write-Host "========================================" -ForegroundColor Cyan
Write-Host " Push Backend Testing Implementation" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "📍 Current directory: $(Get-Location)" -ForegroundColor Gray
Write-Host ""

Write-Host "🔍 Checking git status..." -ForegroundColor Yellow
try {
    git status
    Write-Host ""
} catch {
    Write-Host "❌ Error: Git is not available or this is not a git repository" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "➕ Adding all new test files..." -ForegroundColor Yellow
try {
    git add team28HackathonAPI/team28HackathonAPITests/
    git add team28HackathonAPI/run-tests.bat
    git add team28HackathonAPI/run-tests.ps1
    git add README.md
    git add push-backend-tests.bat
    git add push-backend-tests.ps1
    Write-Host "✅ Files staged successfully" -ForegroundColor Green
} catch {
    Write-Host "❌ Error staging files" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "📋 Checking what will be committed..." -ForegroundColor Yellow
git status
Write-Host ""

Write-Host "💾 Committing changes..." -ForegroundColor Yellow
$commitMessage = @"
feat: Add comprehensive backend testing suite

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

Technologies: xUnit, Moq, FluentAssertions, ASP.NET Core Testing
"@

try {
    git commit -m $commitMessage
    Write-Host "✅ Changes committed successfully" -ForegroundColor Green
} catch {
    Write-Host ""
    Write-Host "⚠️  No changes to commit or commit failed." -ForegroundColor Yellow
    Write-Host "Checking current status..." -ForegroundColor Gray
    git status
    Read-Host "Press Enter to continue anyway or Ctrl+C to exit"
}

Write-Host ""
Write-Host "🚀 Pushing to remote repository..." -ForegroundColor Yellow
Write-Host "Creating and pushing 'israel' branch..." -ForegroundColor Gray

try {
    git push -u origin israel
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "✅ SUCCESS! Backend tests pushed to GitHub" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "🎉 Your changes have been pushed to the 'israel' branch." -ForegroundColor Green
    Write-Host ""
    Write-Host "📋 Next steps:" -ForegroundColor Cyan
    Write-Host "  1. Create a Pull Request on GitHub" -ForegroundColor Gray
    Write-Host "  2. Merge with the main branch" -ForegroundColor Gray
    Write-Host "  3. Share the testing implementation with your team" -ForegroundColor Gray
    Write-Host ""
    Write-Host "🔗 Repository: https://github.com/Blossom-A/M-R-Hackathon-team-28-REPO" -ForegroundColor Blue
    Write-Host "🌿 Branch: israel" -ForegroundColor Blue
} catch {
    Write-Host ""
    Write-Host "❌ Push failed. This might be because:" -ForegroundColor Red
    Write-Host "  1. You need to authenticate with GitHub" -ForegroundColor Yellow
    Write-Host "  2. The remote repository is not accessible" -ForegroundColor Yellow
    Write-Host "  3. Network connectivity issues" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "🔧 You can try pushing manually with:" -ForegroundColor Cyan
    Write-Host "  git push -u origin israel" -ForegroundColor Gray
    Write-Host ""
}

Read-Host "Press Enter to exit"
