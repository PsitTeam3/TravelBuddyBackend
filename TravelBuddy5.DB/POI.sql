CREATE TABLE [dbo].[POI]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Description] TEXT NOT NULL, 
    [Coordinates] [sys].[geography] NULL, 
    [VisitDuration] INT NOT NULL 
)
