﻿CREATE TABLE [dbo].[TourPOI]
(
	[FK_Tour] INT NOT NULL , 
    [FK_POI] INT NOT NULL, 
    [Order] INT NOT NULL, 
    PRIMARY KEY ([FK_POI], [FK_Tour]), 
    CONSTRAINT [FK_TourPOI_Tour] FOREIGN KEY ([FK_Tour]) REFERENCES [Tour]([Id]),
	CONSTRAINT [FK_TourPOI_POI] FOREIGN KEY ([FK_POI]) REFERENCES [POI]([Id])
)
