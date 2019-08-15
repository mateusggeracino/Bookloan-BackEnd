CREATE TABLE Loan
  (
	Id integer identity(1,1),
	UniqueKey uniqueidentifier default newid(),
	Days integer not null,
	ClientId integer not null,
	BookId integer not null,
	RegisterDate datetime
  );

  ALTER TABLE Loan
  ADD CONSTRAINT PK_LoanId PRIMARY KEY (Id);

  ALTER TABLE Loan
  ADD CONSTRAINT FK_ClientId_Loan FOREIGN KEY (ClientId) REFERENCES Client(Id);

  ALTER TABLE Loan
  ADD CONSTRAINT FK_BookId_Loan FOREIGN KEY (BookId) REFERENCES Book(Id);