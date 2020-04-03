﻿CREATE TABLE [dbo].[Cookies]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [ImageUrl] NVARCHAR(150) NULL, 
    [Price] FLOAT NULL
)
