CREATE TABLE [dbo].[UserTour]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FK_Tour] INT NOT NULL,
	[FK_User] INT NOT NULL,
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NULL, 
    CONSTRAINT [FK_UserTour_Tour] FOREIGN KEY ([FK_Tour]) REFERENCES [Tour]([Id]),
	CONSTRAINT [FK_UserTour_User] FOREIGN KEY ([FK_User]) REFERENCES [User]([Id])
)
