
INSERT INTO MyParties.[dbo].[Users] (fio, birthday, email, [role])
VALUES ('Vasja', '1988-08-06', 'qwe@qw.ru', 1),
('MAsha', '2000-08-06', 'wer@qw.ru', 2)

INSERT INTO MyParties.[dbo].Contacts ( phone, email)
VALUES ( '876543158', 'ert@qw.ru'),
( '12332121', 'sdf@qw.ru')

INSERT INTO MyParties.[dbo].Sponsors ([name], [location])
VALUES ('Спонсор 1', 'Минск'),
('Спонсор 2', 'Москва')

INSERT INTO MyParties.[dbo].Parties (title, [location], [date], ownerId)
VALUES ('Супер вечеринка', 'Минск', '2019-11-30', 1),
('Новый год', 'Париж', '2019-12-31', 2)

INSERT INTO MyParties.[dbo].SponsorsParties (partyId, sponsorId)
VALUES (1, 2),
(2, 1),
(1, 1)

INSERT INTO MyParties.[dbo].Participants ([name], attend, reason, partyId, userId)
VALUES ('Первый участник', 1, '' , 1 , 1),
('Гонщик', 0, 'уехал' , 1 , 2),
('Первый участник', 1, '' , 2 , 1),
('Гонщик', 1, '' , 2 , 2)