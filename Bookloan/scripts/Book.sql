
  CREATE TABLE Book
  (
	Id integer identity(1,1),
	UniqueKey uniqueidentifier default newid(),
	Title varchar(90) not null,
	Author varchar(100) not null,
	RegisterDate datetime
  );

  ALTER TABLE Book
  ADD CONSTRAINT PK_BookId PRIMARY KEY (Id)