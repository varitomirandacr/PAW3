CREATE TABLE [dbo].[Tasks] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255)  NULL,
    [Description]  NVARCHAR (1000) NULL,
    [Status]       NVARCHAR (50)   NULL,
    [DueDate]      DATETIME        NULL,
    [CreatedAt]    DATETIME        NULL,
    [LastModified] DATETIME        NULL,
    [ModifiedBy]   NVARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

