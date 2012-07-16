USE [Master]
IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE name = 'Nisan')
CREATE database [Nisan]
GO

USE [Nisan]
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Users]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Users] (
	[Id]					int IDENTITY,
	[Code]					varchar,
	[Name]					varchar NOT NULL,
	[Password]				varchar,
	[Email]					varchar,
	[Phone]					varchar
CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
)
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Stocks]') AND OBJECTPROPERTY(id, N'IsTable') = 1)
CREATE TABLE [Stocks] (
	[Id]					int IDENTITY,
	[Type]					varchar NOT NULL,
	[Price]					decimal NOT NULL
CONSTRAINT [PK_Stocks] PRIMARY KEY ([Id])
)
GO


