GO
create database Курсовая_АбрамовТакси
GO

use Курсовая_АбрамовТакси
GO
create table Водители
(
Код_Водителя INT PRIMARY KEY NOT NULL IDENTITY,
ФИО nvarchar(50) not null,
ГосНомер nvarchar(15) not null,
МаркаАвто nvarchar(30) not null,
ЦветАвто nvarchar(30) not null,
Статус nvarchar(30)
);
GO
INSERT INTO Водители VALUES
('Иван Иванович Иванов', 'Н838ТУ', 'Mercedes Benz', 'Серый', 'Свободен'),
('Вишняков Ростислав Антонинович', 'А231АЕ', 'Datsun', 'Белый', 'Свободен'),
('Аксёнов Семен Оскарович', 'В583МУ', 'Skoda Octavia', 'Синий', 'Свободен'),
('Прохоров Соломон Петрович', 'И124АР', 'Hyundai Sonata', 'Желтый', 'Свободен'),
('Анисимов Даниил Георгиевич', 'А242АУ', 'Haval F7', 'Синий', 'Свободен'),
('Елисеев Мирон Натанович', 'Р231ПУ', 'Honda', 'Черный', 'Свободен'),
('Беспалов Петр Куприянович', 'И232МУ', 'BMW X7', 'Серый', 'Свободен'),
('Сафонов Ким Максимович', 'Д265ЦУ', 'Volvo S60', 'Красный', 'Свободен'),
('Щербаков Ефим Лукьевич', 'Х232ХА', 'Genesis G90', 'Черный', 'Свободен'),
('Крюков Лукьян Филатович', 'Л213ЛА', 'Chevrolet Monza', 'Серый', 'Свободен'),
('Гущин Виктор Тихонович', 'П168ПП', 'Audi S4', 'Красный', 'Свободен'),
('Гуляев Ипполит Антонинович', 'О156ПО', 'Audi S2', 'Синий', 'Свободен'),
('Дементьев Лаврентий Тимофеевич', 'В275ВУ', 'Москвич', 'Красный', 'Свободен'),
('Селиверстов Мирон Феликсович', 'Е314ЕУ', 'Chevrolet Camaro', 'Желтый', 'Свободен'),
('Рожков Исак Парфеньевич', 'Ч246ЧМ', 'Mercedes Benz', 'Черный', 'Свободен');

GO
create table УлицаПосадки
(
КодУлицыПосадки INT PRIMARY KEY NOT NULL IDENTITY,
Название nvarchar(50) not null
);
GO
create table РайонУлицыПосадки
(
Код_РайонаПосадки INT PRIMARY KEY NOT NULL IDENTITY,
Код_УлицыПосадки INT foreign key references УлицаПосадки(КодУлицыПосадки),
Название nvarchar(50) NOT NULL
);
GO
create table УлицаВысадки
(
Код_УлицыВысадки INT PRIMARY KEY NOT NULL IDENTITY,
Название nvarchar(50) not null
);
GO
create table РайонВысадки
(
Код_РайонаВысадки INT PRIMARY KEY NOT NULL IDENTITY,
Код_УлицыВысадки INT foreign key references УлицаВысадки(Код_УлицыВысадки),
Название nvarchar(50) not null
);
GO
create table СвязьРайонов
(
Код_Связи INT PRIMARY KEY NOT NULL IDENTITY,
Код_РайонаПосадки int foreign key references РайонУлицыПосадки(Код_РайонаПосадки),
Код_РайонаВысадки int foreign key references РайонВысадки(Код_РайонаВысадки),
Время_Поездки time not null,
Стоимость int not null
);
GO
create table Купоны
(
Id_Купона int primary key not null identity,
Купон nvarchar(6),
Помечание nvarchar(30)
);
GO
create table Заявки
(
Код_Заявки int primary key identity,
ФИО_Клиента nvarchar(50) not null,
Номер_Клиента nvarchar(50) not null,
Время_Заявки date not null,
Улица_Посадки nvarchar(50) not null,
Район_Посадки nvarchar(50) not null,
НомерДомаПосадки nvarchar(50) not null,
Улица_Высадки nvarchar(50) not null,
Район_Высадки nvarchar(50) not null,
НомерДомаВысадки nvarchar(50) not null,
Статус nvarchar(20) not null
);
GO
create table Обслуживание_Заявок
(
Код_Обслуживания int primary key identity,
Код_Заявки int foreign key references Заявки(Код_Заявки) not null,
Код_Водителя int foreign key references Водители(Код_Водителя) not null,
Дата_Приема_Заявки date,
Время_начала time,
Дата_Окончания date,
Время_Окончания time,
Купон nvarchar(50),
Цена nvarchar(50)
);
GO
create table Пользователи(
Id_Клиента int not null PRIMARY KEY IDENTITY,
Номер_Телефона nvarchar(20) not null,
ФИО nvarchar(40) not null,
Пароль nvarchar(20) not null,
Роль nvarchar(30)
);
GO
INSERT INTO Пользователи VALUES
('+7-982-962-0169','Абрамов Руслан','123456',''),
('+7-905-821-0368','Диспетчер Абрамов','111111','Диспетчер');