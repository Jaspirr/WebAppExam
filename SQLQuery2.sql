CREATE TABLE Categories (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(MAX)
);
GO

CREATE TABLE Products (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    LgImgUrl NVARCHAR(MAX),
    SmImgUrl NVARCHAR(MAX)
);
GO

CREATE TABLE ProductsCategories (
    ProductId INT,
    CategoryId INT,
    PRIMARY KEY (ProductId, CategoryId),
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
GO