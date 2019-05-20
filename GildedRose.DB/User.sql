CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(100) NULL, 
    [Email] VARCHAR(150) NOT NULL, 
    [Address] NVARCHAR(250) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NULL, 
    [CityId] INT NULL, 
    [PostalCode] VARCHAR(50) NULL, 
    [PasswordHash] VARCHAR(250) NOT NULL, 
    [ExpiresAt] DATETIME NOT NULL, 
    [Salt] NVARCHAR(250) NOT NULL, 
    [AddedAt] DATETIME NOT NULL, 
    CONSTRAINT [AK_User_Email] UNIQUE ([Email]), 
    CONSTRAINT [FK_User_City] FOREIGN KEY ([CityId]) REFERENCES [City]([Id])
)
