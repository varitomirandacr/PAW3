CREATE TABLE [dbo].[Suppliers] (
    [SupplierID]   INT             IDENTITY (1, 1) NOT NULL,
    [SupplierName] NVARCHAR (255)  NULL,
    [ContactName]  NVARCHAR (255)  NULL,
    [ContactTitle] NVARCHAR (255)  NULL,
    [Phone]        NVARCHAR (255)  NULL,
    [Address]      NVARCHAR (1000) NULL,
    [City]         NVARCHAR (255)  NULL,
    [Country]      NVARCHAR (255)  NULL,
    [LastModified] DATETIME        NULL,
    [ModifiedBy]   NVARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([SupplierID] ASC)
);

