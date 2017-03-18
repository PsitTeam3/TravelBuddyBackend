CREATE TABLE [dbo].[Tour]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Description] VARCHAR(200) NOT NULL, 
    [DetailDescription] TEXT NOT NULL, 
    [FK_City] INT NOT NULL, 
    CONSTRAINT [FK_Tour_City] FOREIGN KEY (FK_City) REFERENCES [City]([Id]), 
)
