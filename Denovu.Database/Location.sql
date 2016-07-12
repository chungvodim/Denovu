CREATE TABLE [dbo].[Location]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(256) NULL,
	[LocalizedName] NVARCHAR(256) NULL,
	[Longitude] FLOAT NULL,
	[Latitude] FLOAT NULL,
	[ParentId] INT NULL,
)
