CREATE TABLE [dbo].[Components] (
    [ID]      NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [name]    NVARCHAR (50)  NOT NULL,
    [content] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Components] PRIMARY KEY CLUSTERED ([ID] ASC)
);

