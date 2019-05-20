CREATE TABLE [dbo].[Purchase]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [PurchasedAt] DATETIME NOT NULL, 
    CONSTRAINT [FK_Purchase_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
