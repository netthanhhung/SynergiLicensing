
--Confirm your version is correct before continuing.
SELECT TOP 1 Version FROM dbo.TagVersion ORDER BY DateCreated DESC, Version DESC

BEGIN TRY

	BEGIN TRANSACTION

    DECLARE 
        @C_CurrentUser varchar (128)
        , @C_CurrentDate datetime 
        , @C_ApplicationId uniqueidentifier
    SELECT 
        @C_CurrentUser = 'Synergi IT - Manual'
        , @C_CurrentDate = getDate()			
	
	IF NOT EXISTS (SELECT * FROm aspnet_Applications)
	BEGIN
		INSERT INTO [dbo].[aspnet_Applications]
           ([ApplicationName],[LoweredApplicationName],[ApplicationId],[Description])
		VALUES
           ('/','/',NEWID(), NULL)
           
        INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('common', 1, 1)
		INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('health monitoring', 1, 1)
		INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('membership', 1, 1)
		INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('personalization', 1, 1)
		INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('profile', 1, 1)
		INSERT INTO [dbo].[aspnet_SchemaVersions]([Feature],[CompatibleSchemaVersion],[IsCurrentVersion])
		VALUES('role manager', 1, 1)
	END	
	
	SELECT @C_ApplicationId = ApplicationId FROM aspnet_Applications
	
	IF NOT EXISTS (SELECT * FROM Country)
	BEGIN
		INSERT INTO Country (Name, DateCreated, CreatedBy)
		SELECT Name, GETDATE(), 'Synergi-IT-Manual'
		FROM TimeClock.dbo.Country
	END
	
	IF NOT EXISTS (SELECT * FROM Module)
	BEGIN
		INSERT INTO Module (Name, Description, Price, DateCreated, CreatedBy)
		SELECT 'Administration', 'Administration', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Sales', 'Sales', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Yield', 'Yield', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Energy', 'Energy', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Employees', 'Employees', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Operations', 'Operations', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Guests', 'Guests', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Budget', 'Budget', 10000.00, @C_CurrentDate, @C_CurrentUser
	END
	
	IF NOT EXISTS (SELECT * FROM CompetitorSource)
	BEGIN
		INSERT INTO CompetitorSource (Name, Description, Price, DateCreated, CreatedBy)
		SELECT 'Wotif', 'Wotif', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Xpedia', 'Xpedia', 10000.00, @C_CurrentDate, @C_CurrentUser
		UNION ALL SELECT 'Agoda', 'Agoda', 10000.00, @C_CurrentDate, @C_CurrentUser
	END
	
	IF NOT EXISTS (SELECT * FROM [aspnet_Users] WHERE UserId = 'A56F648B-E943-4BCF-82A5-D3D0C4E0D52E')
	BEGIN
		
		INSERT INTO [dbo].[aspnet_Users]
           ([ApplicationId],[UserId],[UserName],[LoweredUserName],[MobileAlias],[IsAnonymous],[LastActivityDate])
		VALUES
           (@C_ApplicationId,'A56F648B-E943-4BCF-82A5-D3D0C4E0D52E'
           ,'PortalAdmin', 'portaladmin',NULL, 0, @C_CurrentDate)

		INSERT INTO [dbo].[aspnet_Membership]
           ([ApplicationId],[UserId],[Password],[PasswordFormat],[PasswordSalt],[MobilePIN],[Email]
           ,[LoweredEmail],[PasswordQuestion],[PasswordAnswer],[IsApproved],[IsLockedOut],[CreateDate]
           ,[LastLoginDate],[LastPasswordChangedDate],[LastLockoutDate],[FailedPasswordAttemptCount]
           ,[FailedPasswordAttemptWindowStart],[FailedPasswordAnswerAttemptCount]
           ,[FailedPasswordAnswerAttemptWindowStart],[Comment])
		VALUES
           (@C_ApplicationId,'A56F648B-E943-4BCF-82A5-D3D0C4E0D52E','BCuyFePubV9cRKBDNX9BYLM3sUI='
           ,1,'DRveBflXiP5pYyS15MMGGQ==',NULL,NULL,NULL,NULL,NULL,1,0,@C_CurrentDate,@C_CurrentDate
           ,@C_CurrentDate,@C_CurrentDate,0,@C_CurrentDate,0,@C_CurrentDate,NULL)
		
	END
	
	IF NOT EXISTS (SELECT TagVersionID FROM TagVersion WHERE Version = '1.0.0.1')
	BEGIN
		INSERT INTO dbo.TagVersion (TagName, Version, CreatedBy)
		VALUES ('201203211430_TagVersion_v1.0.0.1', '1.0.0.1', 'HT')
	END
	COMMIT TRANSACTION 
	PRINT 'EXECUTED SUCCESSFULLY'

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION 
	PRINT	'ERROR_NUMBER ' + CONVERT(NVARCHAR(50),ERROR_NUMBER())
	PRINT	'ERROR_SEVERITY '+			CONVERT(NVARCHAR(50),ERROR_SEVERITY())
	PRINT	'ERROR_LINE	' +CONVERT(NVARCHAR(50),ERROR_LINE())
	PRINT	'ERROR_MESSAGE '+	ERROR_MESSAGE()
	PRINT 'EXECUTED UNSUCCESSFULLY'
END CATCH
GO

SELECT TOP 1 Version FROM dbo.TagVersion ORDER BY DateCreated DESC, Version DESC
GO
