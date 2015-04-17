CREATE TABLE [dbo].[StockType] (
    [StockTypeId]          INT            NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (255) NULL,
    [AmountValuePrecision] INT            CONSTRAINT [DF_StockType_AmountValuePrecision] DEFAULT ((0)) NOT NULL,
    [Created]              DATETIME       CONSTRAINT [DF_StockType_Created] DEFAULT (getdate()) NOT NULL,
    [Modified]             DATETIME       CONSTRAINT [DF_StockType_Modified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_StockType] PRIMARY KEY CLUSTERED ([StockTypeId] ASC)
);