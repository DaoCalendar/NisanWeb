USE [Master]
IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE name = 'Nisan')
CREATE database [Nisan]
GO

USE [Nisan]

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Addresses]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Addresses] (
	[Id]					int IDENTITY,
	[Street]				nvarchar,
	[Postal]				varchar,
	[State]					nvarchar,
	[Remarks]				nvarchar,
CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Users]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Users] (
	[Id]					int IDENTITY,
	[Code]					varchar,
	[Name]					nvarchar NOT NULL,
	[Password]				nvarchar,
	[Email]					varchar,
	[Phone]					varchar,
	[AddressId]				int,
	[Remarks]				nvarchar,
CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Users] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Stocks]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Stocks] (
	[Id]					int IDENTITY,
	[Type]					nvarchar NOT NULL,
	[Price]					decimal NOT NULL,
	[Remarks]				nvarchar,
CONSTRAINT [PK_Stocks] PRIMARY KEY ([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Nisans]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Nisans] (
	[Id]					int IDENTITY,
	[Type]					int NOT NULL,
	[Name]					nvarchar NOT NULL,
	[Jawi]					nvarchar,
	[Born]					datetime,
	[Death]					datetime,
	[Deathm]				datetime,
	[Age]					smallint,
	[Remarks]				nvarchar,
CONSTRAINT [PK_Nisans] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Nisans] FOREIGN KEY ([Type]) REFERENCES [Stocks]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Transactions]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Transactions] (
	[Id]					int IDENTITY,
	[Type]					smallint NOT NULL,	--TransactionType
	[No]					varchar,
	[CreatedAt]				datetime NOT NULL,
	[CreatedBy]				int NOT NULL,
	[Reference]				nvarchar,
	[Remarks]				nvarchar,
CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Transactions] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TransactionItems]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [TransactionItems] (
	[Id]					int IDENTITY,
	[Type]					smallint NOT NULL,	--0=Order, 1=Payment, 2=Commission, 3=Delivery
	[Parent]				int NOT NULL,
	[Amount]				decimal,
	[Remarks]				nvarchar,
CONSTRAINT [PK_TransactionItems] PRIMARY KEY ([Id]),
CONSTRAINT [FK_TransactionItems] FOREIGN KEY ([Parent]) REFERENCES [Transactions]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Orders]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Orders] (
	[Id]					int NOT NULL,		--TransactionItems.Id
	[NisanId]				int NOT NULL,		--Nisans.Id
	[Status]				smallint NOT NULL,	--TransactionStage
CONSTRAINT [FK_Orders1] FOREIGN KEY ([Id])	REFERENCES [TransactionItems]([Id]),
CONSTRAINT [FK_Orders2] FOREIGN KEY ([NisanId]) REFERENCES [Nisans]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Commissions]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Commissions] (
	[Id]					int NOT NULL,	--Orders.Id
	[UserId]				int NOT NULL,	--Users.Id
	[Amount]				decimal DEFAULT 0
)
GO

/******* Initial default data in database for use ******/
