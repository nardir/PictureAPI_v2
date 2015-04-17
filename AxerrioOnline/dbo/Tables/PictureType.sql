CREATE TABLE [dbo].[PictureType] (
    [PictureTypeId]       INT            NOT NULL,
    [Name]                NVARCHAR (100) NOT NULL,
    [Created]             DATETIME       CONSTRAINT [DF_PictureType_Created] DEFAULT (getdate()) NOT NULL,
    [Modified]            DATETIME       CONSTRAINT [DF_PictureType_Modified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PictureType] PRIMARY KEY CLUSTERED ([PictureTypeId] ASC)
);
GO