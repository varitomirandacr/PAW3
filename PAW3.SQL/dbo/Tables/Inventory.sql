CREATE TABLE [dbo].[Inventory] (
    [InventoryID]  INT             IDENTITY (1, 1) NOT NULL,
    [UnitPrice]    DECIMAL (10, 2) NULL,
    [UnitsInStock] INT             NULL,
    [LastUpdated]  DATETIME        NULL,
    [ProductId]    INT             NULL,
    [DateAdded]    DATETIME        NULL,
    [ModifiedBy]   NVARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([InventoryID] ASC)
);

