CREATE TABLE [dbo].[Tour]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Country] VARCHAR(10) NOT NULL, 
    [Region] VARCHAR(100) NOT NULL, 
    [Description] VARCHAR(200) NOT NULL, 
    [DetailDescription] TEXT NOT NULL
)
