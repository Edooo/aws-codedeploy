-- comment
CREATE TABLE [NewExtranetDB].[dbo].[PublicApiRole] (
  Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Name NVARCHAR(20) NOT NULL UNIQUE
);
