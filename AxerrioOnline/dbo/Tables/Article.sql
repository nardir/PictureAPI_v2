CREATE TABLE [dbo].[Article] (
    [ArticleKey]                 INT             IDENTITY (1, 1) NOT NULL,
    [Description]                NVARCHAR (255)  NULL,
    [LongDescription]            NVARCHAR (4000) NULL,
    [Code]                       NVARCHAR (50)   NOT NULL,
    [Active]                     BIT             CONSTRAINT [DF_Article_Active] DEFAULT ((1)) NOT NULL,
    [Deleted]                    BIT             CONSTRAINT [DF_Article_Deleted] DEFAULT ((0)) NOT NULL,
    [Created]                    DATETIME        CONSTRAINT [DF_Article_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [Modified]                   DATETIME        CONSTRAINT [DF_Article_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([ArticleKey] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Code]
    ON [dbo].[Article]([Code] ASC);
GO