USE msdb;
GO

EXEC sp_add_job 
    @job_name = 'Daily Upsert Accounts Job', 
    @enabled = 1;

EXEC sp_add_jobstep
    @job_name = 'Daily Upsert Accounts Job',
    @step_name = 'Run UpsertAccountsFromSource',
    @subsystem = 'TSQL',
    @command = 'EXEC [WemaAnalytics].[UpsertAccountsFromSource];',
    @database_name = 'WemaAnalyticsDbs'; 

EXEC sp_add_schedule 
    @schedule_name = 'Daily 2AM Schedule',
    @freq_type = 4,   
    @freq_interval = 1,
    @active_start_time = 020000;  

EXEC sp_attach_schedule 
    @job_name = 'Daily Upsert Accounts Job',
    @schedule_name = 'Daily 2AM Schedule';

EXEC sp_add_jobserver 
    @job_name = 'Daily Upsert Accounts Job';
