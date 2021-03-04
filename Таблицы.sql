GO
create database ��������_������������
GO

use ��������_������������
GO
create table ��������
(
���_�������� INT PRIMARY KEY NOT NULL IDENTITY,
��� nvarchar(50) not null,
�������� nvarchar(15) not null,
��������� nvarchar(30) not null,
�������� nvarchar(30) not null,
������ nvarchar(30)
);
GO
INSERT INTO �������� VALUES
('���� �������� ������', '�838��', 'Mercedes Benz', '�����', '��������'),
('�������� ��������� �����������', '�231��', 'Datsun', '�����', '��������'),
('������ ����� ���������', '�583��', 'Skoda Octavia', '�����', '��������'),
('�������� ������� ��������', '�124��', 'Hyundai Sonata', '������', '��������'),
('�������� ������ ����������', '�242��', 'Haval F7', '�����', '��������'),
('������� ����� ���������', '�231��', 'Honda', '������', '��������'),
('�������� ���� �����������', '�232��', 'BMW X7', '�����', '��������'),
('������� ��� ����������', '�265��', 'Volvo S60', '�������', '��������'),
('�������� ���� ��������', '�232��', 'Genesis G90', '������', '��������'),
('������ ������ ���������', '�213��', 'Chevrolet Monza', '�����', '��������'),
('����� ������ ���������', '�168��', 'Audi S4', '�������', '��������'),
('������ ������� �����������', '�156��', 'Audi S2', '�����', '��������'),
('��������� ��������� ����������', '�275��', '�������', '�������', '��������'),
('����������� ����� ����������', '�314��', 'Chevrolet Camaro', '������', '��������'),
('������ ���� �����������', '�246��', 'Mercedes Benz', '������', '��������');

GO
create table ������������
(
��������������� INT PRIMARY KEY NOT NULL IDENTITY,
�������� nvarchar(50) not null
);
GO
create table �����������������
(
���_������������� INT PRIMARY KEY NOT NULL IDENTITY,
���_������������ INT foreign key references ������������(���������������),
�������� nvarchar(50) NOT NULL
);
GO
create table ������������
(
���_������������ INT PRIMARY KEY NOT NULL IDENTITY,
�������� nvarchar(50) not null
);
GO
create table ������������
(
���_������������� INT PRIMARY KEY NOT NULL IDENTITY,
���_������������ INT foreign key references ������������(���_������������),
�������� nvarchar(50) not null
);
GO
create table ������������
(
���_����� INT PRIMARY KEY NOT NULL IDENTITY,
���_������������� int foreign key references �����������������(���_�������������),
���_������������� int foreign key references ������������(���_�������������),
�����_������� time not null,
��������� int not null
);
GO
create table ������
(
Id_������ int primary key not null identity,
����� nvarchar(6),
��������� nvarchar(30)
);
GO
create table ������
(
���_������ int primary key identity,
���_������� nvarchar(50) not null,
�����_������� nvarchar(50) not null,
�����_������ date not null,
�����_������� nvarchar(50) not null,
�����_������� nvarchar(50) not null,
���������������� nvarchar(50) not null,
�����_������� nvarchar(50) not null,
�����_������� nvarchar(50) not null,
���������������� nvarchar(50) not null,
������ nvarchar(20) not null
);
GO
create table ������������_������
(
���_������������ int primary key identity,
���_������ int foreign key references ������(���_������) not null,
���_�������� int foreign key references ��������(���_��������) not null,
����_������_������ date,
�����_������ time,
����_��������� date,
�����_��������� time,
����� nvarchar(50),
���� nvarchar(50)
);
GO
create table ������������(
Id_������� int not null PRIMARY KEY IDENTITY,
�����_�������� nvarchar(20) not null,
��� nvarchar(40) not null,
������ nvarchar(20) not null,
���� nvarchar(30)
);
GO
INSERT INTO ������������ VALUES
('+7-982-962-0169','������� ������','123456',''),
('+7-905-821-0368','��������� �������','111111','���������');