Create Database Weather_Info
GO
Use Weather_Info
GO
create table States (id int identity(1,1) primary key,Name varchar(100),Woeid int)
GO
create table Cities (id int identity(1,1) primary key,Name varchar(100),state_id int,Woeid int)
GO
create table WeatherInfo (id int identity(1,1) primary key,Title varchar(450),Description varchar(max),Date varchar(30),Temperature int,city_id int)

