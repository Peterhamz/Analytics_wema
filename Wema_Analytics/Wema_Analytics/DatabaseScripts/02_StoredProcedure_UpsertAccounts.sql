CREATE OR ALTER PROCEDURE [WemaAnalytics].[UpsertAccountsFromSource]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @inserts INT = 0, @updates INT = 0;

    INSERT INTO [WemaAnalytics].[Accounts] (account_number, account_name, balance, last_updated_at)
    SELECT s.account_number, s.account_name, s.balance, s.last_updated_at
    FROM [Wema_Analytics_Data].[dbo].[Account_Statistics_Details] s
    LEFT JOIN [WemaAnalytics].[Accounts] d
        ON s.account_number = d.account_number
    WHERE d.account_number IS NULL;

    SET @inserts = @@ROWCOUNT;

    UPDATE d
    SET d.account_name = s.account_name,
        d.balance = s.balance,
        d.last_updated_at = s.last_updated_at
    FROM [WemaAnalytics].[Accounts] d
    INNER JOIN [Wema_Analytics_Data].[dbo].[Account_Statistics_Details] s
        ON d.account_number = s.account_number
    WHERE s.last_updated_at > d.last_updated_at;

    SET @updates = @@ROWCOUNT;

    INSERT INTO [WemaAnalytics].[ProcessRunLogs] (process_name, inserts_count, updates_count)
    VALUES ('UpsertAccountsFromSource', @inserts, @updates);
END;
