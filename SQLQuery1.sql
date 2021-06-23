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
create table Account
(
	Username varchar(30) primary key,
	Password varchar(30),
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
	Fimage nvarchar(50),
	FDescribe nvarchar(200),
	FStatus bit,
	FPrice float,
	CID int references Category(CateID)
)
go
create table Promotional
(
	ID int identity primary key,
	Promoprice float,
	Expirationdate date,
	FoodID int references Food(FoodID)
)
go 
create table Bill
(
	BillID int identity primary key,
	BDate date,
	BTotal float,
	BCart bit,
	CusID int references Customer(CusID)
)
go
create table Detail
(
	DetailID int identity primary key,
	DAmount int ,
	DFName nvarchar(30),
	DfPrice float,
	DFoodID int references Food(FoodID),
	DOderID int references Bill(BillID)
)
