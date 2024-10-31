# Transaction Process

## Note on Architecture Choice
Due to my short timeframe, I implemented this solution as a basic ASP.NET MVC project with Clean Architecture principles. In a production environment, I would typically implement this as an ASP.NET Web API with Vertical Slice Architecture using CQRS & Mediator pattern, paired with a modern frontend framework for better separation of concerns and scalability.

## Note on Git Strategy
For this example project, I opted to push directly to the main branch due to the time constraints and scope of the assignment. In a production environment, I would implement a proper branching strategy (e.g., GitFlow or trunk-based development) with feature branches, pull requests, and code reviews.

## Project Overview
This project implements a transaction processing service that can:
- Upload and process transaction data from CSV and XML files
- Store transaction records in a database
- Query transactions by various criteria (currency, date range, status)
- Validate and log transaction processing results

## Features
- File upload (CSV/XML) with validation
- Transaction data processing and storage
- API endpoints for querying transactions
- Status mapping between different formats
- Error handling and validation
- Audit fields (CreateDate, CreateUser)

## Technical Stack
- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0
- SQL Server LocalDB
- Serilog for structured logging
- xUnit for unit testing
- Moq for mocking in tests

## Project Structure
The solution follows Clean Architecture principles with three main projects:

### TransactionProcess.Core
- Contains domain entities
- Defines interfaces
- Business logic abstractions

### TransactionProcess.Infrastructure
- Entity Framework DbContext
- Repository implementations
- File processing services
- Database migrations

### TransactionProcess.Web
- MVC Controllers
- Views
- View Models
- API endpoints

## Running the Application

1. Clone the repository
2. Ensure SQL Server LocalDB is installed
3. Open the solution in Visual Studio
4. Run the database update command mentioned below
5. Build and run the application

### Requirements
- .NET 8.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 or compatible IDE

## Database Setup
The application uses SQL Server LocalDB with the following connection string:
```
Server=(localdb)\\mssqllocaldb;Database=TransactionProcessDb;Trusted_Connection=True;
```

To update the database, use the following command in the Developer Command Prompt from the solution folder:
```
dotnet ef database update --project TransactionProcess.Infrastructure --startup-project TransactionProcess.Web
```


## API Endpoints
The application provides the following API endpoints for filtering transactions:

- By Currency: `https://localhost:7140/currency/{currencyCode}`
  Example: `https://localhost:7140/currency/usd`

- By Date Range: `https://localhost:7140/date-range/{startDate}/{endDate}`
  Example: `https://localhost:7140/date-range/2016-12-01/2020-12-31`

- By Status: `https://localhost:7140/status/{statusCode}`
  Example: `https://localhost:7140/status/r`

Note: Replace `localhost:7140` with the appropriate host and port if different in your local setup.

## Testing
- Unit tests are implemented using xUnit
- Mock objects are used for external dependencies

Note: Currently, the test data is not properly isolated due to the ApplicationDbContext's seed data being included in the InMemoryDatabase provider. A potential fix for this issue would be:
- Set ASPNETCORE_ENVIRONMENT to "Test" when running tests
- In ApplicationDbContext, add a condition to only seed data when the environment is not "Test"
- Alternatively, create a separate test context without seed data for testing purposes

## Logging
- Structured logging implemented with Serilog
- Logs are written to both console and file
- Log files are stored in the 'logs' directory with daily rolling