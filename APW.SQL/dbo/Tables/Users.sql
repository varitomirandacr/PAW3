CREATE TABLE [dbo].[Users] (
    [UserID]         INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (255) NULL,
    [Email]          NVARCHAR (255) NULL,
    [PasswordHash]   NVARCHAR (255) NULL,
    [CreatedAt]      DATETIME       NULL,
    [IsActive]       BIT            NULL,
    [LastModified]   DATETIME       NULL,
    [ModifiedBy]     NVARCHAR (255) NULL,
    [RoleID]         INT            NULL,
    [LastModifiedBy] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

