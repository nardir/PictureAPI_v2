CREATE TABLE [dbo].[PictureArticle] (
    [PictureKey] INT      NOT NULL,
    [ArticleKey] INT      NOT NULL,
    [SortOrder]  INT      NULL,
    [Created]    DATETIME CONSTRAINT [DF_PictureArticle_Created] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PictureArticle] PRIMARY KEY CLUSTERED ([PictureKey] ASC, [ArticleKey] ASC),
    CONSTRAINT [FK_PictureArticle_Picture] FOREIGN KEY ([PictureKey]) REFERENCES [dbo].[Picture] ([PictureKey]) ON DELETE CASCADE,
    CONSTRAINT [FK_PictureArticle_Article] FOREIGN KEY ([ArticleKey]) REFERENCES [dbo].[Article] ([ArticleKey]) ON DELETE CASCADE
);