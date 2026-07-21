IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260720225038_linemanager'
)
BEGIN

                    IF EXISTS (
                        SELECT 1 FROM sys.foreign_keys 
                        WHERE name = 'FK_Projects_Clients_ClientId' 
                        AND parent_object_id = OBJECT_ID('Projects')
                    )
                        ALTER TABLE Projects DROP CONSTRAINT FK_Projects_Clients_ClientId;
                
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260720225038_linemanager'
)
BEGIN

                    IF EXISTS (
                        SELECT 1 FROM sys.indexes 
                        WHERE name = 'IX_Projects_ClientId' 
                        AND object_id = OBJECT_ID('Projects')
                    )
                        DROP INDEX IX_Projects_ClientId ON Projects;
                
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260720225038_linemanager'
)
BEGIN

                    IF EXISTS (
                        SELECT 1 FROM sys.columns 
                        WHERE name = 'ClientId1' 
                        AND object_id = OBJECT_ID('Projects')
                    )
                        ALTER TABLE Projects DROP COLUMN ClientId1;
                
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260720225038_linemanager'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260720225038_linemanager', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721195450_fixclientprojects'
)
BEGIN
    ALTER TABLE [Projects] DROP CONSTRAINT [FK_Projects_Clients_ClientId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721195450_fixclientprojects'
)
BEGIN
    DROP INDEX [IX_Projects_ClientId] ON [Projects];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721195450_fixclientprojects'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'ClientId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Projects] DROP COLUMN [ClientId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721195450_fixclientprojects'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260721195450_fixclientprojects', N'9.0.0');
END;

COMMIT;
GO

