CREATE TABLE [WemaAnalytics].[Accounts] (
    account_number NVARCHAR(50) PRIMARY KEY,
    account_name NVARCHAR(100),
    balance DECIMAL(18,2),
    last_updated_at DATETIME
);

CREATE TABLE [WemaAnalytics].[ProcessRunLogs] (
    id INT IDENTITY(1,1) PRIMARY KEY,
    process_name NVARCHAR(100),
    inserts_count INT,
    updates_count INT,
    run_at DATETIME DEFAULT GETDATE()
);
