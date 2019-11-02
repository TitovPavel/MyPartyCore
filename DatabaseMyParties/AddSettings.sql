CREATE TABLE MyPartiesEF.[dbo].Settings
(id int IDENTITY NOT NULL CONSTRAINT Settings_Id_PK PRIMARY KEY,
[key] varchar(50) NOT NULL, 
[value] varchar(100) NOT NULL);