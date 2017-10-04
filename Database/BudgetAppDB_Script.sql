
use master
;
go 

/* Database Creation */

create database BudgetAppDB
on primary
(
	-- File name
	name = 'BudgetAppDB',
	-- Data Path and file name
	filename = 'D:\BudgetDB\BudgetAppDB.mdf',
	-- File size
	size = 20MB,
	-- file growth
	filegrowth = 2MB,
	-- maximum file size
	maxsize = 200MB
)
log on
(
	-- File name
	name = 'BudgetAppDBP_log',
	-- Data Path and file name
	filename = 'D:\BudgetDB\BudgetAppDB_log.ldf',
	-- File size
	size = 6MB,
	-- file growth
	filegrowth = 10%,
	-- maximum file size
	maxsize = 25MB
)

use BudgetAppDB
;
go

/* Table Creation */

create table Accounts
(
	AccountId int identity(1,1) not null,
	AccountNumber BigInt not null,
	AccountType nvarchar(40) not null,
	Balance money not null,
	constraint pk_Accounts primary key clustered (AccountId asc)
)
;
go

create table Category
(
	CategoryId int identity(1,1) not null,
	CategoryType nvarchar(40) not null,
	constraint pk_Category primary key clustered (CategoryId asc)
)
;
go

create table Tag
(
	TagId int identity(1,1) not null,
	Description nvarchar(200) not null,
	constraint pk_Tag primary key clustered (TagId asc)
)
;
go

create table Records
(
	RecordId int identity(1,1) not null,
	Date datetime not null,
	Amount money not null,
	Document varbinary(max),
	AccountId int not null,
	CategoryId int not null,
	Constraint pk_Records primary key clustered (RecordId asc)
)
;
go

create table InterTag
(
	InterId int identity(1,1) not null,
	TagId int not null,
	RecordId int not null,
	Constraint pk_InterTag primary key clustered (InterId asc)
)
;
go

/* Adding Forgein Keys */

alter table Records
add
	constraint fk_Records_Accounts foreign key (AccountId)
	references Accounts (AccountId),
	constraint fk_Records_Category foreign key (CategoryId)
	references Category (CategoryId)
;
go

alter table InterTag
add
	constraint fk_InterTag_Tag foreign key (TagId)
	references Tag (TagId),
	constraint fk_InterTag_Records foreign key (RecordId)
	references Records (RecordId)
;
go