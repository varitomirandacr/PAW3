CREATE TABLE [dbo].[Notifications] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [user_id]    INT            NOT NULL,
    [message]    NVARCHAR (255) NOT NULL,
    [is_read]    BIT            DEFAULT ((0)) NULL,
    [created_at] DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

