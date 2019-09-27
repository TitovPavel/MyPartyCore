
--Список всех вечеринок
SELECT [id]
,[title]
,[location]
,[date]
,[ownerId]
FROM [MyParties].[dbo].[Parties]

--список вечеринок, которые еще не прошли
SELECT [id]
,[title]
,[location]
,[date]
,[ownerId]
FROM [MyParties].[dbo].[Parties]
WHERE [date] > CURRENT_TIMESTAMP

--список 5 ближайших вечеринок
SELECT TOP (5) [id]
,[title]
,[location]
,[date]
,[ownerId]
FROM [MyParties].[dbo].[Parties]
WHERE [date] > CURRENT_TIMESTAMP
ORDER BY [date]


--список всех проголосовавших об участии в вечеринке
SELECT [id]
,[name]
,[attend]
,[reason]
,[avatarId]
,[partyId]
,[userId]
FROM [MyParties].[dbo].[Participants]
WHERE [partyId] = 1

--Получение списка идущих на вечеринку в алфавитном порядке по имени
SELECT Participants.[id]
,[name]
,[attend]
,[reason]
,[avatarId]
,[partyId]
,[userId]
FROM [MyParties].[dbo].[Participants] Participants
INNER JOIN [MyParties].[dbo].[Users] [Users] ON Participants.[userId] = [Users].id
WHERE [partyId] = 2
ORDER BY [Users].FIO

--Обновление адреса вечеринки
UPDATE [MyParties].[dbo].[Parties] SET [location] = 'Лондон'
WHERE [location] = 'Минск'

--*Получение списка организаторов вечеринок и количества организованных вечеринок
SELECT [Users].[id]
,MAX([fio])
,COUNT(Parties.id)
FROM [MyParties].[dbo].[Parties] Parties
INNER JOIN [MyParties].[dbo].[Users] [Users] ON Parties.[ownerId] = [Users].id
GROUP BY [Users].[id]
