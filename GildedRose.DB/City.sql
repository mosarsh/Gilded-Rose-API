CREATE TABLE [dbo].[City]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[CountryId] INT NOT NULL,
    [Name] VARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_City_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)
