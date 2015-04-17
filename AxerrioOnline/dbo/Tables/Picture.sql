CREATE TABLE [dbo].[Picture] (
    [PictureKey]    INT             IDENTITY (1, 1) NOT NULL,
    [PictureTypeId] INT             NOT NULL,
    [UrlSmall]      NVARCHAR (1024) NULL,
    [UrlMedium]     NVARCHAR (1024) NULL,
    [UrlLarge]      NVARCHAR (1024) NULL,
    [Deleted]       BIT             CONSTRAINT [DF_Picture_Deleted_1] DEFAULT ((0)) NOT NULL,
    [External]      BIT             CONSTRAINT [DF_Picture_External] DEFAULT ((0)) NOT NULL,
    [Created]       DATETIME        CONSTRAINT [DF_Picture_Created] DEFAULT (getdate()) NOT NULL,
    [Modified]      DATETIME        CONSTRAINT [DF_Picture_Modified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED ([PictureKey] ASC),
    CONSTRAINT [FK_Picture_PictureType] FOREIGN KEY ([PictureTypeId]) REFERENCES [dbo].[PictureType] ([PictureTypeId])
);
GO
CREATE NONCLUSTERED INDEX [IX_FK_Picture_PictureType]
    ON [dbo].[Picture]([PictureTypeId] ASC);
GO