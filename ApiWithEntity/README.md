# Commands for testing project
## Database schema
To build the database schema run the following SQL script.
~~~sql
create database apiwithentity;

use apiwithentity;

create table contactTypes (
	id int primary key identity(1,1),
	description varchar(50)
);

create table contacts (
	id int primary key identity(1,1),
	name varchar(30),
	description varchar(30),
	phone varchar(10),
	contactTypeId int,
	constraint fkContactTypeId foreign key(contactTypeId) references contactTypes(id)
);
~~~
## Database context scaffold
To setup the database context run the following shell command.

Important! Change the `Server` value of the connection string.
~~~shell
Scaffold-DbContext "Server={server};Database=ApiWithEntity;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
~~~
