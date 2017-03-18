CREATE TABLE [dbo].[City]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [FK_Country] INT NOT NULL, 
    CONSTRAINT [FK_City_Country] FOREIGN KEY ([FK_Country]) REFERENCES [Country]([Id])
)
