/**********************************Drop Foreign Keys*********************************************************/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContactInformation_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContactInformation]'))
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [FK_ContactInformation_Country]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contract_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contract]'))
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [FK_Contract_Customer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ModulePackage_Package]') AND parent_object_id = OBJECT_ID(N'[dbo].[ModulePackage]'))
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [FK_ModulePackage_Package]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ModulePackage_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[ModulePackage]'))
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [FK_ModulePackage_Module]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Contract_Package]') AND parent_object_id = OBJECT_ID(N'[dbo].[Contract]'))
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [FK_Contract_Package]
GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractModule_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractModule]'))
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [FK_ContractModule_Module]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractModule_Contract]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractModule]'))
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [FK_ContractModule_Contract]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractCompetitorSource_CompetitorSource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractCompetitorSource]'))
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [FK_ContractCompetitorSource_CompetitorSource]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractCompetitorSource_Contract]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractCompetitorSource]'))
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [FK_ContractCompetitorSource_Contract]


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TagVersio__DateC__401C279A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TagVersion] DROP CONSTRAINT [DF__TagVersio__DateC__401C279A]
END

GO
/***********************************************************************************************************/
/**********************************Table TagVersion*********************************************************/
/****** Object:  Table [dbo].[TagVersion]    Script Date: 03/21/2012 14:36:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TagVersion]') AND type in (N'U'))
DROP TABLE [dbo].[TagVersion]
GO
CREATE TABLE [dbo].[TagVersion](
	[TagVersionID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](128) NULL,
	[Version] [varchar](128) NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_TagVersion] PRIMARY KEY CLUSTERED 
(
	[TagVersionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TagVersion] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

/***********************************************************************************************************/
/**********************************Table Country*********************************************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_DateCreated]
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_DateUpdated]
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_CreatedBy]
END
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_UpdatedBy]
END
GO


/****** Object:  Table [dbo].[Country]    Script Date: 03/20/2012 16:26:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO

CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[DateCreated] [smalldatetime] NULL,
	[DateUpdated] [smalldatetime] NULL,
	[CreatedBy] [varchar](128) NULL,
	[UpdatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteCountry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteCountry]
GO
CREATE PROCEDURE [dbo].[procDeleteCountry](@CountryId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE Country WHERE CountryId = @CountryId
END
GO

/****** Object:  StoredProcedure [dbo].[procListCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListCountry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListCountry]
GO
CREATE PROCEDURE [dbo].[procListCountry] (@CountryId int = NULL)

AS
BEGIN
	SET NOCOUNT ON

	SELECT CountryID, Name, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM Country
	WHERE (@CountryID IS NULL OR CountryId = @CountryId)
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveCountry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveCountry]
GO
CREATE PROCEDURE [dbo].[procSaveCountry]

	@CountryId int OUTPUT,
	@Name varchar(50),
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @CountryID IS NULL BEGIN

		INSERT INTO Country(
			Name ,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@Name ,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @CountryId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE Country SET

			Name = @Name ,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE CountryId = @CountryId

	END

	SELECT Concurrency FROM Country WHERE CountryId = @CountryId
END
GO
/*******************************************************************************************************/
/**********************************Table Contact*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_FirstName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_FirstName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_LastName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_LastName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_AddressLineOne]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_Table_1_AddressLineOne]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_Address2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_Address2]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_AddressLineTwo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_Table_1_AddressLineTwo]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_State]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_State]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_Postcode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_Postcode]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_Country]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_Country]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_PhoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_PhoneNumber]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_FaxNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_FaxNumber]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_Email]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_Email]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInformation_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInformation] DROP CONSTRAINT [DF_ContactInformation_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[ContactInformation]    Script Date: 03/20/2012 16:25:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInformation]') AND type in (N'U'))
DROP TABLE [dbo].[ContactInformation]
GO
CREATE TABLE [dbo].[ContactInformation](
	[ContactInformationId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](128) NOT NULL,
	[LastName] [varchar](128) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Address2] [varchar](255) NOT NULL,
	[City] [varchar](255) NOT NULL,
	[State] [varchar](255) NOT NULL,
	[Postcode] [varchar](50) NOT NULL,
	[CountryId] [int] NULL,
	[PhoneNumber] [varchar](128) NOT NULL,
	[FaxNumber] [varchar](128) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
	[HistoricalId] [int] NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[ContactInformationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ContactInformation]  WITH CHECK ADD  CONSTRAINT [FK_ContactInformation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[ContactInformation] CHECK CONSTRAINT [FK_ContactInformation_Country]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FirstName]  DEFAULT ('') FOR [FirstName]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_LastName]  DEFAULT ('') FOR [LastName]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineOne]  DEFAULT ('') FOR [Address]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Address2]  DEFAULT ('') FOR [Address2]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineTwo]  DEFAULT ('') FOR [City]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_State]  DEFAULT ('') FOR [State]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Postcode]  DEFAULT ('') FOR [Postcode]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Country]  DEFAULT ('') FOR [CountryId]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FaxNumber]  DEFAULT ('') FOR [FaxNumber]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Email]  DEFAULT ('') FOR [Email]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteContactInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteContactInformation]
GO
CREATE PROCEDURE [dbo].[procDeleteContactInformation](@ContactInformationId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE ContactInformation WHERE ContactInformationId = @ContactInformationId
END
GO

/****** Object:  StoredProcedure [dbo].[procListContactInformation]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListContactInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListContactInformation]
GO
CREATE PROCEDURE [dbo].[procListContactInformation] (@ContactInformationId int = NULL)

AS
BEGIN
	SET NOCOUNT ON

	SELECT ContactInformationID, FirstName, LastName, Address, Address2, City, State, Postcode, CountryId, 
			PhoneNumber, FaxNumber, Email, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM ContactInformation
	WHERE (@ContactInformationID IS NULL OR ContactInformationId = @ContactInformationId)
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveContactInformation]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveContactInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveContactInformation]
GO
CREATE PROCEDURE [dbo].[procSaveContactInformation]

	@ContactInformationID int OUTPUT,
	@Address varchar(255),
	@City varchar(255),
	@State varchar(255),
	@Postcode varchar(50),
	@CountryID int,
	@PhoneNumber varchar(128),
	@FaxNumber varchar(128),
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @ContactInformationID IS NULL BEGIN

		INSERT INTO ContactInformation(
			Address ,
			City ,
			State ,
			Postcode ,
			CountryID ,
			PhoneNumber ,
			FaxNumber ,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@Address ,
			@City ,
			@State ,
			@Postcode ,
			@CountryID ,
			@PhoneNumber ,
			@FaxNumber ,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @ContactInformationID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE ContactInformation SET

			Address = @Address ,
			City = @City ,
			State = @State ,
			Postcode = @Postcode ,
			CountryID = @CountryID ,
			PhoneNumber = @PhoneNumber ,
			FaxNumber = @FaxNumber ,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContactInformationID = @ContactInformationID

	END

	SELECT Concurrency FROM ContactInformation WHERE ContactInformationID = @ContactInformationID
END
GO

/*******************************************************************************************************/
/**********************************Table Customer*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_BusinessName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_BusinessName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF_Customer_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[Customer]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO

CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[BusinessName] [varchar](128) NOT NULL,
	[ContactInformationId] [int] NULL,
	[ShippingContactInformationId] [int] NULL,
	[IsLegacy] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_BusinessName]  DEFAULT ('') FOR [BusinessName]
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteCustomer]
GO
CREATE PROCEDURE [dbo].[procDeleteCustomer](@CustomerId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE Customer WHERE CustomerId = @CustomerId
END
GO

/****** Object:  StoredProcedure [dbo].[procListCustomer]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListCustomer]
GO
CREATE PROCEDURE [dbo].[procListCustomer] 
(
@CustomerId int = NULL
, @Name varchar(128)
, @IncludeLegacy bit 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		C.CustomerID
		, C.[Name]
		, C.BusinessName
		, C.ContactInformationId
		, C.ShippingContactInformationId
		, C.IsLegacy
		, C.Concurrency
		, C.DateCreated
		, C.DateUpdated
		, C.CreatedBy
		, C.UpdatedBy
		FROM 
			Customer AS C		
		WHERE 
			(@CustomerId IS NULL OR C.CustomerID = @CustomerId)
			AND (@Name IS NULL OR @Name = '' OR C.Name like '%' + @Name + '%' 
					OR C.BusinessName like '%' + @Name + '%')
			AND (C.IsLegacy = 0 OR @IncludeLegacy = 1)
		ORDER BY 
			C.[Name]
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveCustomer]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveCustomer]
GO
CREATE PROCEDURE [dbo].[procSaveCustomer]
(
 @CustomerID int OUTPUT
, @Name varchar(128)
, @BusinessName varchar(128)
, @ContactInformationId int = NULL
, @ShippingContactInformationId int = NULL
, @IsLegacy bit
, @CurrentUser varchar(128)
)
AS
BEGIN

	SET NOCOUNT ON

	IF @CustomerID IS NULL
	BEGIN
	
		INSERT INTO [Customer]
			(
			[Name]
			, [BusinessName]
			, [ContactInformationId]
			, [ShippingContactInformationId]
			, [IsLegacy]	
			, [DateCreated] 
			, [DateUpdated] 
			, [CreatedBy] 
			, [UpdatedBy] 
			)
			VALUES
			(
			@Name
			, @BusinessName
			, @ContactInformationId
			, @ShippingContactInformationId
			, @IsLegacy	
			, GETDATE() 
			, GETDATE() 
			, @CurrentUser 
			, @CurrentUser 
			)
			
		SET @CustomerID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN

		UPDATE 
			[Customer]
		SET
			[Name] = @Name
			, [BusinessName] = @BusinessName
			, [ContactInformationId] = @ContactInformationId
			, [ShippingContactInformationId] = @ShippingContactInformationId
			, [IsLegacy] = @IsLegacy	
			, [DateUpdated] = GETDATE()
			, [UpdatedBy] = @CurrentUser
		WHERE
			[CustomerID] = @CustomerID

	END

	SELECT [Concurrency] FROM [Customer] WHERE [CustomerID] = @CustomerID
	
END
GO
/*******************************************************************************************************/
/**********************************Table Module*********************************************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Module_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Module_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Module_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Module_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[Module]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
DROP TABLE [dbo].[Module]
GO

CREATE TABLE [dbo].[Module](
	[ModuleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Price] [decimal](20,8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [DF_Module_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteModule]
GO
CREATE PROCEDURE [dbo].[procDeleteModule](@ModuleId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE Module WHERE ModuleId = @ModuleId
END
GO

/****** Object:  StoredProcedure [dbo].[procListModule]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListModule]
GO
CREATE PROCEDURE [dbo].[procListModule] (@ModuleId int = NULL)

AS
BEGIN
	SET NOCOUNT ON

	SELECT ModuleID, Name, Description, Price, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM Module
	WHERE (@ModuleID IS NULL OR ModuleId = @ModuleId)
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveModule]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveModule]
GO
CREATE PROCEDURE [dbo].[procSaveModule]

	@ModuleId int OUTPUT,
	@Name varchar(128),
	@Description varchar(max),
	@Price decimal(20,8),
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @ModuleID IS NULL BEGIN

		INSERT INTO Module(
			Name,
			Description,
			Price,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@Name,
			@Description,
			@Price,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @ModuleID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE Module SET

			Name = @Name,
			Description = @Description,
			Price = @Price,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ModuleID = @ModuleID

	END

	SELECT Concurrency FROM Module WHERE ModuleID = @ModuleId
END
GO
/*******************************************************************************************************/
/**********************************Table Package*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Package_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Package] DROP CONSTRAINT [DF_Package_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Package_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Package] DROP CONSTRAINT [DF_Package_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Package_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Package] DROP CONSTRAINT [DF_Package_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Package_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Package] DROP CONSTRAINT [DF_Package_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[Package]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Package]') AND type in (N'U'))
DROP TABLE [dbo].[Package]
GO

CREATE TABLE [dbo].[Package](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Price] [decimal](20,8) NULL,
	[IsLegacy] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeletePackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeletePackage]
GO
CREATE PROCEDURE [dbo].[procDeletePackage](@PackageId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE Package WHERE PackageId = @PackageId
END
GO

/****** Object:  StoredProcedure [dbo].[procListPackage]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListPackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListPackage]
GO
CREATE PROCEDURE [dbo].[procListPackage] (@PackageId int = NULL, @IncludeLegacy bit)

AS
BEGIN
	SET NOCOUNT ON

	SELECT PackageID, Name, Description, Price, IsLegacy, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy,
			CASE WHEN NOT EXISTS
				(
				SELECT * FROM Contract C
				WHERE C.PackageId = P.PackageId
				)
			THEN 1
			ELSE 0
			END AS CanDelete
	FROM Package P
	WHERE (@PackageID IS NULL OR PackageId = @PackageId)
	AND (P.IsLegacy = 0 OR @IncludeLegacy = 1)
	ORDER BY P.Name
END
GO

/****** Object:  StoredProcedure [dbo].[procSavePackage]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSavePackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSavePackage]
GO
CREATE PROCEDURE [dbo].[procSavePackage]

	@PackageId int OUTPUT,
	@Name varchar(128),
	@Description varchar(max),
	@Price decimal(20,8),
	@IsLegacy bit,
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @PackageId IS NULL BEGIN

		INSERT INTO Package(
			Name,
			Description,
			Price,
			IsLegacy,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@Name,
			@Description,
			@Price,
			@IsLegacy,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @PackageID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE Package SET

			Name = @Name,
			Description = @Description,
			Price = @Price,
			IsLegacy = @IsLegacy,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE PackageId = @PackageId

	END

	SELECT Concurrency FROM Package WHERE PackageId = @PackageId
END
GO
/*******************************************************************************************************/
/**********************************Table ModulePackage*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ModulePackage_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [DF_ModulePackage_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ModulePackage_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [DF_ModulePackage_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ModulePackage_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [DF_ModulePackage_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ModulePackage_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ModulePackage] DROP CONSTRAINT [DF_ModulePackage_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[ModulePackage]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ModulePackage]') AND type in (N'U'))
DROP TABLE [dbo].[ModulePackage]
GO

CREATE TABLE [dbo].[ModulePackage](
	[ModulePackageId] [int] IDENTITY(1,1) NOT NULL,
	[PackageId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ModulePackage] PRIMARY KEY CLUSTERED 
(
	[ModulePackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ModulePackage] ADD  CONSTRAINT [DF_ModulePackage_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ModulePackage] ADD  CONSTRAINT [DF_ModulePackage_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[ModulePackage] ADD  CONSTRAINT [DF_ModulePackage_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[ModulePackage] ADD  CONSTRAINT [DF_ModulePackage_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[ModulePackage]  WITH NOCHECK ADD  CONSTRAINT [FK_ModulePackage_Package] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([PackageId])
GO

ALTER TABLE [dbo].[ModulePackage] CHECK CONSTRAINT [FK_ModulePackage_Package]
GO

ALTER TABLE [dbo].[ModulePackage]  WITH NOCHECK ADD  CONSTRAINT [FK_ModulePackage_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
GO

ALTER TABLE [dbo].[ModulePackage] CHECK CONSTRAINT [FK_ModulePackage_Module]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteModulePackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteModulePackage]
GO
CREATE PROCEDURE [dbo].[procDeleteModulePackage](@PackageId int, @ModuleId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE ModulePackage WHERE PackageId = @PackageId AND ModuleId = @ModuleId
END
GO

/****** Object:  StoredProcedure [dbo].[procListModulePackage]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListModulesOfPackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListModulesOfPackage]
GO
CREATE PROCEDURE [dbo].[procListModulesOfPackage] 
(
	@PackageId int
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT M.ModuleId, M.Name, M.Description, M.Price,
		M.Concurrency, M.DateCreated, M.DateUpdated, M.CreatedBy, M.UpdatedBy
	FROM Module M
	INNER JOIN ModulePackage MP on MP.ModuleId = M.ModuleId
	WHERE MP.PackageId = @PackageId
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveModulePackage]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveModulePackage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveModulePackage]
GO
CREATE PROCEDURE [dbo].[procSaveModulePackage]	
	@PackageId int,
	@ModuleId int,
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM ModulePackage WHERE PackageId = @PackageId AND ModuleId = @ModuleId)
	BEGIN

		INSERT INTO ModulePackage(
			PackageId,
			ModuleId,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@PackageId,
			@ModuleId,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)


	END ELSE BEGIN

		UPDATE ModulePackage SET

			PackageId = @PackageId,
			ModuleId = @ModuleId,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE PackageId = @PackageId AND ModuleId = @ModuleId

	END

	SELECT Concurrency FROM ModulePackage WHERE PackageId = @PackageId AND ModuleId = @ModuleId
END
GO

/******************************************************************************************************/
/**********************************Table Contract*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Contract_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [DF_Contract_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Contract_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [DF_Contract_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Contract_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [DF_Contract_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Contract_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Contract] DROP CONSTRAINT [DF_Contract_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[Contract]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contract]') AND type in (N'U'))
DROP TABLE [dbo].[Contract]
GO

CREATE TABLE [dbo].[Contract](
	[ContractId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] int NOT NULL,
	[LicenseKey] [uniqueidentifier] NOT NULL,
	[Date] smalldatetime NOT NULL,	
	[DomainName] nvarchar(max),
	[SecondDomainName] nvarchar(max),
	[ActivatedDate] smalldatetime NOT NULL,	
	[ExpiredDate] smalldatetime NULL,	
	[RealEndDate] smalldatetime NULL,	
	[NbrSites] int NOT NULL,
	[Description] varchar(max),
	[PackageId] int NOT NULL,
	[TotalPrice] decimal(20,8) NOT NULL,
	[IsLegacy] bit NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Contract] ADD  CONSTRAINT [DF_Contract_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[Contract] ADD  CONSTRAINT [DF_Contract_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[Contract] ADD  CONSTRAINT [DF_Contract_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[Contract] ADD  CONSTRAINT [DF_Contract_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[Contract]  WITH NOCHECK ADD  CONSTRAINT [FK_Contract_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO

ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Customer]
GO

ALTER TABLE [dbo].[Contract]  WITH NOCHECK ADD  CONSTRAINT [FK_Contract_Package] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([PackageId])
GO

ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Package]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteContract]
GO
CREATE PROCEDURE [dbo].[procDeleteContract](@ContractId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE Contract WHERE ContractId = @ContractId
END
GO

/****** Object:  StoredProcedure [dbo].[procListContract]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListContract]
GO
CREATE PROCEDURE [dbo].[procListContract] 
(
	@ContractId int = NULL
	, @CustomerId int = NULL
	, @LicenseKey uniqueidentifier = NULL
	, @DomainName varchar(128)
	, @CustomerName varchar(128)
	, @PackageId int = NULL
	, @IncludeLegacy bit 
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT C.ContractID, C.CustomerId, C.LicenseKey, C.Date, C.DomainName, C.SecondDomainName,
	    C.ActivatedDate, C.ExpiredDate, C.RealEndDate, C.NbrSites, C.Description, C.PackageId, C.TotalPrice, C.IsLegacy,
		C.Concurrency, C.DateCreated, C.DateUpdated, C.CreatedBy, C.UpdatedBy
	FROM Contract C
	INNER JOIN Customer CU ON CU.CustomerId = C.CustomerId
	INNER JOIN Package P on P.PackageId = C.PackageId
	
	WHERE (@ContractId IS NULL OR C.ContractId = @ContractId)
	AND (@CustomerId IS NULL OR C.CustomerId = @CustomerId)
	AND (@PackageId IS NULL OR P.PackageId = @PackageId)
	AND (@LicenseKey IS NULL OR C.LicenseKey = @LicenseKey)
	AND (@DomainName IS NULL OR @DomainName = '' OR C.DomainName LIKE LOWER('%' + @DomainName + '%'))
	AND (@CustomerName IS NULL OR @CustomerName = '' OR CU.Name LIKE LOWER('%' + @CustomerName + '%'))
	AND (C.IsLegacy = 0 OR @IncludeLegacy= 1)
	
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveContract]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveContract]
GO
CREATE PROCEDURE [dbo].[procSaveContract]

	@ContractId int OUTPUT,
	@CustomerId int,
	@LicenseKey uniqueidentifier,
	@Date smalldatetime,
	@DomainName varchar(max),
	@SecondDomainName varchar(max),
	@ActivatedDate smalldatetime,
	@ExpiredDate smalldatetime = NULL,
	@RealEndDate smalldatetime = NULL,
	@NbrSites int,
	@Description varchar(max),
	@PackageId int,
	@TotalPrice decimal(20,8),
	@IsLegacy bit,
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @ContractId IS NULL BEGIN

		INSERT INTO Contract(
			CustomerId,
			LicenseKey,
			Date,
			DomainName,
			SecondDomainName,
			ActivatedDate,
			ExpiredDate,
			RealEndDate,
			NbrSites,
			Description,
			PackageId,
			TotalPrice,
			IsLegacy,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@CustomerId,
			NEWID(),
			@Date,
			@DomainName,
			@SecondDomainName,
			@ActivatedDate,
			@ExpiredDate,
			@RealEndDate,
			@NbrSites,
			@Description,
			@PackageId,
			@TotalPrice,
			@IsLegacy,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @ContractID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE Contract SET

			CustomerId = @CustomerId,
			LicenseKey = @LicenseKey,
			Date = @Date,
			DomainName = @DomainName,
			SecondDomainName = @SecondDomainName,
			ActivatedDate = @ActivatedDate,
			ExpiredDate = @ExpiredDate,
			RealEndDate = @RealEndDate,
			NbrSites = @NbrSites,
			Description = @Description,
			PackageId = @PackageId,
			TotalPrice = @TotalPrice,
			IsLegacy = @IsLegacy,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContractId = @ContractId

	END

	SELECT Concurrency FROM Contract WHERE ContractId = @ContractId
END
GO


/******************************************************************************************************/
/**********************************Table ContractModule*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractModule_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [DF_ContractModule_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractModule_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [DF_ContractModule_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractModule_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [DF_ContractModule_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractModule_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractModule] DROP CONSTRAINT [DF_ContractModule_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[ContractModule]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractModule]') AND type in (N'U'))
DROP TABLE [dbo].[ContractModule]
GO

CREATE TABLE [dbo].[ContractModule](
	[ContractModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContractModule] PRIMARY KEY CLUSTERED 
(
	[ContractModuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ContractModule] ADD  CONSTRAINT [DF_ContractModule_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ContractModule] ADD  CONSTRAINT [DF_ContractModule_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[ContractModule] ADD  CONSTRAINT [DF_ContractModule_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[ContractModule] ADD  CONSTRAINT [DF_ContractModule_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[ContractModule]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractModule_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
GO

ALTER TABLE [dbo].[ContractModule] CHECK CONSTRAINT [FK_ContractModule_Module]
GO

ALTER TABLE [dbo].[ContractModule]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractModule_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO

ALTER TABLE [dbo].[ContractModule] CHECK CONSTRAINT [FK_ContractModule_Contract]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteContractModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteContractModule]
GO
CREATE PROCEDURE [dbo].[procDeleteContractModule](@ContractId int, @ModuleId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE ContractModule WHERE ContractId = @ContractId AND ModuleId = @ModuleId
END
GO

/****** Object:  StoredProcedure [dbo].[procListAdditionalContractModules]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListAdditionalContractModules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListAdditionalContractModules]
GO
CREATE PROCEDURE [dbo].[procListAdditionalContractModules] 
(
	@ContractId int
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT distinct M.ModuleId, M.Name, M.Description, M.Price,
		M.Concurrency, M.DateCreated, M.DateUpdated, M.CreatedBy, M.UpdatedBy
	FROM Module M
	INNER JOIN ContractModule CM on M.ModuleId = CM.ModuleId
	
	WHERE CM.ContractId = @ContractId
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveContractModule]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveContractModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveContractModule]
GO
CREATE PROCEDURE [dbo].[procSaveContractModule]

	@ContractId int,
	@ModuleId int,
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM ContractModule WHERE ContractId = @ContractId AND ModuleId = @ModuleId)
	BEGIN

		INSERT INTO ContractModule(
			ContractId,
			ModuleId,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@ContractId,
			@ModuleId,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

	END ELSE BEGIN

		UPDATE ContractModule SET

			ContractId = @ContractId,
			ModuleId = @ModuleId,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContractId = @ContractId AND ModuleId = @ModuleId

	END

	SELECT Concurrency FROM ContractModule WHERE ContractId = @ContractId AND ModuleId = @ModuleId
END
GO

/****** Object:  StoredProcedure [dbo].[procListUserName]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListUserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListUserName]
GO
CREATE PROCEDURE [dbo].[procListUserName]
(
	@ApplicationName nvarchar(256), 
	@UserNameToMatch nvarchar(256)
)	
AS
BEGIN
	SET NOCOUNT ON

	DECLARE 
		@ApplicationId uniqueidentifier

    SELECT  
		@ApplicationId = NULL

    SELECT  
		@ApplicationId = ApplicationId 
	FROM 
		dbo.aspnet_Applications 
	WHERE 
		LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0
                
    SELECT 
		DISTINCT
		UserName
    FROM   
		dbo.aspnet_Users U
    WHERE  
		U.ApplicationId = @ApplicationId 
		AND U.LoweredUserName LIKE LOWER(@UserNameToMatch + '%')

    ORDER BY 
		UserName


END
GO


/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserName_Custom]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UpdateUserName_Custom]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[aspnet_Membership_UpdateUserName_Custom]
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserName_Custom]
	@ApplicationName	nvarchar(256),
	@UserName			nvarchar(256),
	@NewUserName		nvarchar(256)
AS
BEGIN
	DECLARE @UserId uniqueidentifier
	DECLARE @ApplicationId uniqueidentifier
	DECLARE @NewLoweredUserName nvarchar(256)
	DECLARE @LastActivityDate datetime

	SELECT  @UserId = NULL
	SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
	FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
	WHERE   LoweredUserName = LOWER(@UserName) AND
			u.ApplicationId = a.ApplicationId  AND
			LOWER(@ApplicationName) = a.LoweredApplicationName AND
			u.UserId = m.UserId

	IF (@UserId IS NULL)
	BEGIN
		SELECT 1
		RETURN
	END

	SET @LastActivityDate = (getdate())
	SET @NewLoweredUserName = LOWER(@NewUserName)

	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END
	ELSE
	SET @TranStarted = 0

	UPDATE dbo.aspnet_Users WITH (ROWLOCK)
	SET
		UserName = @NewUserName,
		LoweredUserName = @NewLoweredUserName,
		LastActivityDate = @LastActivityDate
	WHERE
		@UserId = UserId

	IF( @@ERROR <> 0 )
		GOTO Cleanup

	IF( @TranStarted = 1 )
	BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
	END

	SELECT 0
	RETURN

	Cleanup:

	IF( @TranStarted = 1 )
	BEGIN
		SET @TranStarted = 0
		ROLLBACK TRANSACTION
	END

	SELECT -1
	RETURN
END

GO

/****** Object:  StoredProcedure [dbo].[procListAspUser]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListAspUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListAspUser]
GO
CREATE PROCEDURE [dbo].[procListAspUser]	
	@UserID uniqueidentifier = null
AS
BEGIN
	SET NOCOUNT ON
	SELECT M.ApplicationId,M.UserId,Password,PasswordFormat,PasswordSalt,MobilePIN
		  ,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut
		  ,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate
		  ,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount
		  ,FailedPasswordAnswerAttemptWindowStart,Comment
		  ,U.UserName,U.LoweredUserName,U.MobileAlias, U.IsAnonymous,U.LastActivityDate
	FROM aspnet_Membership M
	INNER JOIN aspnet_Users U on M.UserId = U.UserId
	WHERE (@UserID is null OR M.UserId = @UserID)
	ORDER BY U.UserName
	
	-- ======================================================================
	-- Change History
	-- ====================================================================== 
	-- 11-04-2012 HT
	-- Init : to list aspnet_Users
END
GO

/**********************************Table CompetitorSource*********************************************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CompetitorSource_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CompetitorSource] DROP CONSTRAINT [DF_CompetitorSource_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CompetitorSource_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CompetitorSource] DROP CONSTRAINT [DF_CompetitorSource_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CompetitorSource_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CompetitorSource] DROP CONSTRAINT [DF_CompetitorSource_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CompetitorSource_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CompetitorSource] DROP CONSTRAINT [DF_CompetitorSource_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[CompetitorSource]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompetitorSource]') AND type in (N'U'))
DROP TABLE [dbo].[CompetitorSource]
GO

CREATE TABLE [dbo].[CompetitorSource](
	[CompetitorSourceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Price] [decimal](20,8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_CompetitorSource] PRIMARY KEY CLUSTERED 
(
	[CompetitorSourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CompetitorSource] ADD  CONSTRAINT [DF_CompetitorSource_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[CompetitorSource] ADD  CONSTRAINT [DF_CompetitorSource_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[CompetitorSource] ADD  CONSTRAINT [DF_CompetitorSource_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[CompetitorSource] ADD  CONSTRAINT [DF_CompetitorSource_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteCompetitorSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteCompetitorSource]
GO
CREATE PROCEDURE [dbo].[procDeleteCompetitorSource](@CompetitorSourceId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE CompetitorSource WHERE CompetitorSourceId = @CompetitorSourceId
END
GO

/****** Object:  StoredProcedure [dbo].[procListCompetitorSource]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListCompetitorSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListCompetitorSource]
GO
CREATE PROCEDURE [dbo].[procListCompetitorSource] (@CompetitorSourceId int = NULL)

AS
BEGIN
	SET NOCOUNT ON

	SELECT CompetitorSourceID, Name, Description, Price, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM CompetitorSource
	WHERE (@CompetitorSourceID IS NULL OR CompetitorSourceId = @CompetitorSourceId)
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveCompetitorSource]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveCompetitorSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveCompetitorSource]
GO
CREATE PROCEDURE [dbo].[procSaveCompetitorSource]

	@CompetitorSourceId int OUTPUT,
	@Name varchar(128),
	@Description varchar(max),
	@Price decimal(20,8),
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @CompetitorSourceID IS NULL BEGIN

		INSERT INTO CompetitorSource(
			Name,
			Description,
			Price,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@Name,
			@Description,
			@Price,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @CompetitorSourceID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE CompetitorSource SET

			Name = @Name,
			Description = @Description,
			Price = @Price,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE CompetitorSourceID = @CompetitorSourceID

	END

	SELECT Concurrency FROM CompetitorSource WHERE CompetitorSourceID = @CompetitorSourceId
END
GO
/*******************************************************************************************************/

/**********************************Table ContractCompetitorSource*********************************************************/

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractCompetitorSource_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [DF_ContractCompetitorSource_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractCompetitorSource_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [DF_ContractCompetitorSource_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractCompetitorSource_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [DF_ContractCompetitorSource_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContractCompetitorSource_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContractCompetitorSource] DROP CONSTRAINT [DF_ContractCompetitorSource_UpdatedBy]
END

GO

/****** Object:  Table [dbo].[ContractCompetitorSource]    Script Date: 03/20/2012 16:29:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractCompetitorSource]') AND type in (N'U'))
DROP TABLE [dbo].[ContractCompetitorSource]
GO

CREATE TABLE [dbo].[ContractCompetitorSource](
	[ContractCompetitorSourceId] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[CompetitorSourceId] [int] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContractCompetitorSource] PRIMARY KEY CLUSTERED 
(
	[ContractCompetitorSourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ContractCompetitorSource] ADD  CONSTRAINT [DF_ContractCompetitorSource_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ContractCompetitorSource] ADD  CONSTRAINT [DF_ContractCompetitorSource_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[ContractCompetitorSource] ADD  CONSTRAINT [DF_ContractCompetitorSource_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO

ALTER TABLE [dbo].[ContractCompetitorSource] ADD  CONSTRAINT [DF_ContractCompetitorSource_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[ContractCompetitorSource]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractCompetitorSource_CompetitorSource] FOREIGN KEY([CompetitorSourceId])
REFERENCES [dbo].[CompetitorSource] ([CompetitorSourceId])
GO

ALTER TABLE [dbo].[ContractCompetitorSource] CHECK CONSTRAINT [FK_ContractCompetitorSource_CompetitorSource]
GO

ALTER TABLE [dbo].[ContractCompetitorSource]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractCompetitorSource_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO

ALTER TABLE [dbo].[ContractCompetitorSource] CHECK CONSTRAINT [FK_ContractCompetitorSource_Contract]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteContractCompetitorSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteContractCompetitorSource]
GO
CREATE PROCEDURE [dbo].[procDeleteContractCompetitorSource](@ContractId int, @CompetitorSourceId int)	
AS
BEGIN
	SET NOCOUNT ON

	DELETE ContractCompetitorSource WHERE ContractId = @ContractId AND CompetitorSourceId = @CompetitorSourceId
END
GO

/****** Object:  StoredProcedure [dbo].[procListContractCompetitorSources]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListContractCompetitorSources]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListContractCompetitorSources]
GO
CREATE PROCEDURE [dbo].[procListContractCompetitorSources] 
(
	@ContractId int
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT distinct M.CompetitorSourceId, M.Name, M.Description, M.Price,
		M.Concurrency, M.DateCreated, M.DateUpdated, M.CreatedBy, M.UpdatedBy
	FROM CompetitorSource M
	INNER JOIN ContractCompetitorSource CM on M.CompetitorSourceId = CM.CompetitorSourceId
	
	WHERE CM.ContractId = @ContractId
END
GO

/****** Object:  StoredProcedure [dbo].[procSaveContractCompetitorSource]    Script Date: 03/21/2012 09:52:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveContractCompetitorSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveContractCompetitorSource]
GO
CREATE PROCEDURE [dbo].[procSaveContractCompetitorSource]

	@ContractId int,
	@CompetitorSourceId int,
	@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM ContractCompetitorSource WHERE ContractId = @ContractId AND CompetitorSourceId = @CompetitorSourceId)
	BEGIN

		INSERT INTO ContractCompetitorSource(
			ContractId,
			CompetitorSourceId,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@ContractId,
			@CompetitorSourceId,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

	END ELSE BEGIN

		UPDATE ContractCompetitorSource SET

			ContractId = @ContractId,
			CompetitorSourceId = @CompetitorSourceId,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContractId = @ContractId AND CompetitorSourceId = @CompetitorSourceId

	END

	SELECT Concurrency FROM ContractCompetitorSource WHERE ContractId = @ContractId AND CompetitorSourceId = @CompetitorSourceId
END
GO



