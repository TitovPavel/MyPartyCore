
CREATE TABLE MyParties.[dbo].Images
(id int IDENTITY NOT NULL CONSTRAINT Image_Id_PK PRIMARY KEY,
[fileName] varchar(50) NOT NULL, 
imageData varbinary(MAX) NOT NULL);

CREATE TABLE MyParties.[dbo].[Users] 
(id int IDENTITY NOT NULL CONSTRAINT User_Id_PK PRIMARY KEY,
FIO varchar(50) NOT NULL, 
birthday date NOT NULL, 
email varchar(50) NOT NULL,
[role] int NOT NULL);

CREATE TABLE MyParties.[dbo].Contacts
(id int IDENTITY NOT NULL CONSTRAINT Contact_Id_PK PRIMARY KEY,
phone varchar(50) NOT NULL, 
email varchar(50) NOT NULL);

CREATE TABLE MyParties.[dbo].Sponsors
(id int IDENTITY NOT NULL CONSTRAINT Sponsor_Id_PK PRIMARY KEY FOREIGN KEY REFERENCES Contacts(id),
[name] varchar(50) NOT NULL, 
[location] varchar(50) NOT NULL,
logoId int FOREIGN KEY REFERENCES Images(id));

CREATE TABLE MyParties.[dbo].Parties
(id int IDENTITY NOT NULL CONSTRAINT Party_Id_PK PRIMARY KEY,
title varchar(50) NOT NULL, 
[location] varchar(50) NOT NULL,
[date] datetime NOT NULL,
ownerId int NOT NULL FOREIGN KEY REFERENCES [Users](id));

CREATE TABLE MyParties.[dbo].SponsorsParties
(id int IDENTITY NOT NULL CONSTRAINT SponsorsParties_Id_PK PRIMARY KEY,
partyId int NOT NULL FOREIGN KEY REFERENCES Parties(id),
sponsorId int NOT NULL FOREIGN KEY REFERENCES Sponsors(id));

CREATE TABLE MyParties.[dbo].Participants
(id int IDENTITY NOT NULL CONSTRAINT Participants_Id_PK PRIMARY KEY,
[name] varchar(50) NOT NULL, 
attend bit NOT NULL, 
reason varchar(100) NOT NULL, 
avatarId int FOREIGN KEY REFERENCES Images(id),
partyId int NOT NULL FOREIGN KEY REFERENCES Parties(id),
userId int NOT NULL FOREIGN KEY REFERENCES [Users](id));