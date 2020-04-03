CREATE TABLE [dbo].[OrderLines]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Quantity] INT NULL, 
    [CookieId] UNIQUEIDENTIFIER NULL, 
    [OrderId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [CookieForeignKey] FOREIGN KEY (CookieId) REFERENCES [dbo].[Cookies](Id), 
    CONSTRAINT [OrderForiegnKey] FOREIGN KEY (OrderId) REFERENCES [dbo].[Orders](Id) ON DELETE CASCADE
)
