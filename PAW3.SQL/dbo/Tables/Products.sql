CREATE TABLE [dbo].[Products] (
    [ProductID]    INT             IDENTITY (1, 1) NOT NULL,
    [ProductName]  NVARCHAR (255)  NULL,
    [InventoryID]  INT             NULL,
    [SupplierID]   INT             NULL,
    [Description]  NVARCHAR (1000) NULL,
    [Rating]       DECIMAL (3, 2)  NULL,
    [CategoryID]   INT             NULL,
    [LastModified] DATETIME        NULL,
    [ModifiedBy]   NVARCHAR (255)  NULL,
    [CreatedBy]    NVARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]),
    FOREIGN KEY ([InventoryID]) REFERENCES [dbo].[Inventory] ([InventoryID]),
    FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID])
);

