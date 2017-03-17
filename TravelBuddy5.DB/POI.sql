CREATE TABLE [dbo].[POI]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Description] TEXT NOT NULL, 
    [Coordinates] [sys].[geography] NOT NULL, 
    [VisitDuration] INT NOT NULL 
)
