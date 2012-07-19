USE [Master]
IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE name = 'Nisan')
CREATE database [Nisan]
GO

USE [Nisan]

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Addresses]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Addresses] (
	[Id]					int IDENTITY,
	[Street]				nvarchar(255),
	[Postal]				varchar(10),
	[State]					nvarchar(50),
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Users]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Users] (
	[Id]					int IDENTITY,
	[Code]					varchar(50),
	[Name]					nvarchar(50) NOT NULL,
	[Password]				nvarchar(50),
	[Email]					varchar(50),
	[Phone]					varchar(20),
	[AddressId]				int,
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Users] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id])
)
GO

--TODO: normalize table [Stocks]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Stocks]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Stocks] (
	[Id]					int IDENTITY,
	[Type]					nvarchar(50) NOT NULL,
	[Price]					decimal NOT NULL,
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_Stocks] PRIMARY KEY ([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Nisans]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Nisans] (
	[Id]					int IDENTITY,
	[Type]					int NOT NULL,		--Stock.Id
	[Name]					nvarchar(50) NOT NULL,
	[Jawi]					nvarchar(50),
	[Born]					datetime,
	[Death]					datetime,
	[Deathm]				datetime,
	[Age]					smallint,
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_Nisans] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Nisans] FOREIGN KEY ([Type]) REFERENCES [Stocks]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Transactions]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Transactions] (
	[Id]					int IDENTITY,
	[Type]					smallint NOT NULL,	--TransactionType
	[No]					varchar(20),
	[CreatedAt]				datetime NOT NULL,
	[CreatedBy]				int NOT NULL,
	[Reference]				nvarchar(20),
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
CONSTRAINT [FK_Transactions] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TransactionItems]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [TransactionItems] (
	[Id]					int IDENTITY,
	[Type]					smallint NOT NULL,	--0=Order, 1=Payment, 2=Commission, 3=Delivery
	[Parent]				int NOT NULL,		--Transactions.Id
	[Amount]				decimal,
	[Remarks]				nvarchar(255),
	[Uri]					nvarchar(255),
CONSTRAINT [PK_TransactionItems] PRIMARY KEY ([Id]),
CONSTRAINT [FK_TransactionItems] FOREIGN KEY ([Parent]) REFERENCES [Transactions]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Orders]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Orders] (
	[Id]					int IDENTITY,
	[ItemId]				int NOT NULL,		--TransactionItems.Id
	[NisanId]				int NOT NULL,		--Nisans.Id
	[Status]				smallint NOT NULL,	--TransactionStage
CONSTRAINT [PK_Orders] PRIMARY KEY([Id]),
CONSTRAINT [FK_Orders1] FOREIGN KEY ([ItemId])	REFERENCES [TransactionItems]([Id]),
CONSTRAINT [FK_Orders2] FOREIGN KEY ([NisanId]) REFERENCES [Nisans]([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Commissions]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Commissions] (
	[Id]					int IDENTITY,
	[OrderId]				int NOT NULL,	--Orders.Id
	[UserId]				int NOT NULL,	--Users.Id
	[Amount]				decimal DEFAULT 0,
CONSTRAINT [PK_Commissions] PRIMARY KEY([Id]),
CONSTRAINT [FK_Commissions1] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id]),
CONSTRAINT [FK_Commissions2] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
GO

/******* Initial default data in database for use ******/
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Batik(L)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Batik(P)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Putih(L)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Putih(P)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Hitam(L)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Hitam(P)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Hijau(L)',300);
INSERT INTO Stocks(Type,Price) VALUES('1½'' Batu Hijau(P)',300);

INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Batik(L)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Batik(P)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Putih(L)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Putih(P)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Hitam(L)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Hitam(P)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Hijau(L)',350);
INSERT INTO Stocks(Type,Price) VALUES('2'' Batu Hijau(P)',350);

INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Batik(L)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Batik(P)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Putih(L)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Putih(P)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Hitam(L)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Hitam(P)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Hijau(L)',500);
INSERT INTO Stocks(Type,Price) VALUES('2½'' Batu Hijau(P)',500);