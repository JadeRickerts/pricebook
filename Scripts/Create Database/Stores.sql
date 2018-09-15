﻿/*
Deployment script for C:\USERS\CODEBEAST\DOCUMENTS\VISUAL STUDIO 2015\PROJECTS\PRICEBOOKAPPLICATION\PRICEBOOKAPPLICATION\NEWPRICEBOOK.MDF

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "C:\USERS\CODEBEAST\DOCUMENTS\VISUAL STUDIO 2015\PROJECTS\PRICEBOOKAPPLICATION\PRICEBOOKAPPLICATION\NEWPRICEBOOK.MDF"
:setvar DefaultFilePrefix "C_\USERS\CODEBEAST\DOCUMENTS\VISUAL STUDIO 2015\PROJECTS\PRICEBOOKAPPLICATION\PRICEBOOKAPPLICATION\NEWPRICEBOOK.MDF_"
:setvar DefaultDataPath "C:\Users\CodeBeast\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\CodeBeast\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key  is skipped, element [dbo].[Stores].[Id] (SqlSimpleColumn) will not be renamed to StoreID';


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[Stores]...';


GO
CREATE TABLE [dbo].[Stores] (
    [StoreID]       INT           IDENTITY (100, 1) NOT NULL,
    [StoreName]     NVARCHAR (50) NOT NULL,
    [StoreLocation] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([StoreID] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Update complete.';


GO
