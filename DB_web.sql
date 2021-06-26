create database WEBPROJECT
go 
use WEBPROJECT 
go 

create table Customer
(
	CusID int identity primary key,
	Fullname nvarchar(50),
	Address nvarchar(100),
	Gmail varchar(100),
	Phone varchar(11)
)
go
create table Position
(
	PosID int identity primary key,
	PName Nvarchar(10),
	
)
go
create table Account
(
	Username varchar(30) primary key,
	Password varchar(30),
	Position int references Position(PosID),
	CusID int references Customer(CusID)
)
go

create table Category
(
	CateID int identity primary key,
	CName nvarchar(15)
)
go
create table Food
(
	FoodID int identity primary key,
	FName nvarchar(30),
	Fimage nvarchar(255),
	FDescribe nvarchar(200),
	FStatus bit,
	FPrice int,
	CID int references Category(CateID)
)
go
create table Promotional
(
	ID int identity primary key,
	Promoprice int ,
	Expirationdate date,
	FoodID int references Food(FoodID)
)
go 
create table Bill
(
	BillID int identity primary key,
	BDate date,
	BTotal int,
	BCart bit,
	note nvarchar(500),
	CusID int references Customer(CusID)
)
go
create table Detail
(
	DetailID int identity primary key,
	DAmount int ,
	DFName nvarchar(30),
	DfPrice int,
	DFimg nvarchar(255),
	DFoodID int references Food(FoodID),
	DOderID int references Bill(BillID)
)
