CREATE TABLE Client
(
	Id integer identity(1,1),
	UniqueKey uniqueidentifier default newId() not null,
	Name varchar(80) not null,
	Lastname varchar(80) not null,
	SocialNumber varchar(15) not null,
	Active bit,
	Password varchar(50)
	Email varchar(120) not null,
	Phone varchar(15),
	RegisterDate datetime
);

ALTER TABLE Client
ADD CONSTRAINT PK_CLIENT PRIMARY KEY (Id)