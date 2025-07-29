# Monkey & River 2025 Hackathon - Phase 1
**Team: 28**
**Stack Choice:** ASP.NET Core (C#) + React

## Project Overview
Full-stack application with authentication, database connectivity, and core CRUD functionality.

## Tech Stack
- **Frontend:** React 18+ 
- **Backend:** ASP.NET Core 8.0 Web API
- **Database:** SQL Server/SQLite with Entity Framework Core
- **Authentication:** ASP.NET Identity with JWT tokens

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+
- SQL Server or SQLite

### 1. Clone Repository
```bash
git clone https://github.com/Blossom-A/M-R-Hackathon-team-28-REPO.git
cd M-R-Hackathon-team-28-REPO
```

### 2. Backend Setup

#### Backend Setup Instructions
1. **Ensure your local MSSQL database is running.**

2. **Open the ASP.NET Core Web API solution in Visual Studio or VS Code.**

3. **Configure the database connection string in `appsettings.json`:**
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=team28;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

4. **Apply the latest migration and update the database in the package manager console:**
   ```bash
   add-migration "Initial"
   update-database
   ```

5. **Run the backend server by selecting the https button at the top**

#### Alternative CLI Setup
```bash
cd team28HackathonAPI
dotnet restore
dotnet build
dotnet run
```

### 3. Frontend Setup
```bash
cd frontend
npm install
npm start
```

### 4. Database Setup
Run the SQL script in `db/setup.sql` to create the database schema.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/diagnostictest` | Add a new diagnostic test |
| GET | `/api/diagnostictest` | Get all diagnostic tests |
| GET | `/api/diagnostictest/{id}` | Get a specific test by ID |
| PUT | `/api/diagnostictest/{id}` | Update a specific test |
| DELETE | `/api/diagnostictest/{id}` | Delete a test by ID |
| POST | `/api/auth/login` | Authenticates a user by validating the submitted username and password |

## Testing

### Backend Tests
We have comprehensive backend testing with 42+ tests covering:
- **Unit Tests**: AuthController, WeatherForecastController, Models
- **Integration Tests**: End-to-end API testing
- **Mocking**: HttpContext, Session, ILogger, Database

#### Running Backend Tests

**Option 1: Quick Start (Recommended)**
```bash
cd team28HackathonAPI
# Windows
run-tests.bat

# PowerShell/Cross-platform
./run-tests.ps1
```

**Option 2: Manual .NET CLI**
```bash
cd team28HackathonAPI
dotnet test team28HackathonAPITests/team28HackathonAPITests.csproj --verbosity normal
```

**Option 3: Visual Studio**
1. Open `team28HackathonAPI.sln`
2. Go to **Test** → **Run All Tests**
3. View results in **Test Explorer**

**Option 4: VS Code**
1. Install C# extension
2. Open Command Palette (`Ctrl+Shift+P`)
3. Run **"Test: Run All Tests"**

#### Test Coverage
- ✅ **AuthController**: 8 tests (login validation, session management)
- ✅ **WeatherForecastController**: 10 tests (data structure, mocking)
- ✅ **WeatherForecast Model**: 12 tests (business logic, temperature conversion)
- ✅ **Integration Tests**: 12 tests (end-to-end HTTP testing)

For detailed testing documentation, see `team28HackathonAPI/team28HackathonAPITests/README.md`

### Frontend Tests
```bash
cd frontend
npm test
```
