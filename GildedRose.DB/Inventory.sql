CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InventoryTypeId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(150) NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
	[Stock] INT NOT NULL, 
    [AddedAt] DATETIME NOT NULL, 
    [ModifiedAt] DATETIME NULL,
    CONSTRAINT [FK_Inventory_InventoryType] FOREIGN KEY (InventoryTypeId) REFERENCES [InventoryType]([Id])
)
