CREATE TABLE [dbo].[Roles] (
    [RoleID]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

