CREATE TABLE [Categories]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX)
);

CREATE TABLE [Products]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [LgImgUrl] NVARCHAR(MAX),
    [SmImgUrl] NVARCHAR(MAX)
);

CREATE TABLE [ProductsCategories]
(
    [ProductId] INT NOT NULL,
    [CategoryId] INT NOT NULL,
    PRIMARY KEY ([ProductId], [CategoryId]),
    CONSTRAINT [FK_ProductsCategories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductsCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]) ON DELETE CASCADE
);

INSERT INTO [Categories] ([Name], [Description])
VALUES ('new', NULL),
       ('popular', NULL),
       ('featured', NULL);