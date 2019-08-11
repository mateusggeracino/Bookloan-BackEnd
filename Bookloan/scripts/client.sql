CREATE TABLE Client
(
	Id integer identity(1,1),
	UniqueKey uniqueidentifier default newId() not null,
	Name varchar(80) not null,
	Active bit
);

ALTER TABLE Client
ADD CONSTRAINT PK_CLIENT PRIMARY KEY (Id)