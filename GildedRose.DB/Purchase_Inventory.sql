CREATE TABLE [dbo].[Purchase_Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PurchaseId] INT NOT NULL, 
    [InventoryId] INT NOT NULL, 
    [NumberOfItems] INT NOT NULL, 
    [TotalPrice] DECIMAL(18, 2) NOT NULL, 
    CONSTRAINT [FK_Purchase_Inventory_Inventory] FOREIGN KEY ([InventoryId]) REFERENCES [Inventory]([Id]), 
    CONSTRAINT [FK_Purchase_Inventory_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [Purchase]([Id])
)
