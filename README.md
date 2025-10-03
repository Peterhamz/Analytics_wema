Wema Analytics – CRUD API + Worker Service + SQL Scripts 📌 Project Overview

This solution demonstrates:

CRUD Web API with CQRS & EF Core

Entity: Branch

Commands/Queries: Create, Update, Deactivate, Delete (soft), GetById, Search.

Validations with FluentValidation.

Proper HTTP status codes.

SQL Stored Procedure (UpsertAccountsFromSource)

Reads from [Wema_Analytics_Data].[dbo].[Account_Statistics_Details].

Inserts/updates into [WemaAnalytics].[Accounts].

Logs inserts/updates into [WemaAnalytics].[ProcessRunLogs].

Idempotent & scheduled as a SQL Server Agent Job (daily at 2AM).

Worker Service (KpiWorker)

Runs every 1 minute.

Calculates KPI snapshot and inserts into [WemaAnalytics].[KpiSnapshots].

KPIs: TotalAccounts, ActiveAccounts, TotalBalance, AverageBalance.

Uses EF Core, DI, error logging, cancellation tokens, idempotency.

📂 Project Structure WemaAnalytics/ │── Api/ # ASP.NET Web API with CQRS (Branches CRUD) │── Worker/ # Worker service for KPIs │── SqlScripts/ # SQL scripts for tables, stored procedure, and job │── WemaAnalytics.sln # Solution file │── README.md │── WemaAnalytics.postman_collection.json

🚀 Setup Instructions

Clone Repository git clone https://github.com//Wema_Analytics.git cd Wema_Analytics

Database Setup (SQL Server)

Run the scripts in order from SqlScripts/:

01_Create_Tables.sql – creates Accounts, KpiSnapshots, ProcessRunLogs, etc.

02_UpsertAccountsFromSource.sql – stored procedure.

03_Create_Agent_Job.sql – schedules the SP to run daily at 2AM.

⚠️ Ensure you have a SQL Server instance running and update connection strings.

Configure Connection String
Update appsettings.json in both Api and Worker:

"ConnectionStrings": { "DefaultConnection": "Server=localhost;Database=WemaAnalyticsDbs;Trusted_Connection=True;TrustServerCertificate=True;" }

Run EF Core Migrations
From Visual Studio Terminal or Package Manager Console:

cd Api dotnet ef migrations add InitialCreate dotnet ef database update

Run the Projects
API:

cd Api dotnet run

Swagger UI available at: https://localhost:5001/swagger

Worker:

cd Worker dotnet run

Test with Postman
Use the Postman Collection included: WemaAnalytics.postman_collection.json

Endpoints include:

POST /api/branches/AddBranches

PUT /api/branches/UpdateBranches/{id}

PATCH /api/branches/DeactivateBranches/{id}

DELETE /api/branches/DeleteBranches/{id}

GET /api/branches/GetBranchById/{id}

GET /api/branches/SearchBranches?city=X&region=Y

✅ Deliverables

CRUD API with EF Core + CQRS

SQL Stored Procedure + Agent Job

Worker Service with KPIs

Postman Collection

README with setup instructions

🛠 Technologies

.NET 8 / ASP.NET Core

Entity Framework Core

MediatR (CQRS)

SQL Server

FluentValidation
