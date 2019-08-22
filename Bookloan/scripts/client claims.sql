CREATE TABLE ClientClaims
(
	Id integer identity(1,1),
	ClientId integer,
	ClaimType varchar(80),
	ClaimValue varchar(50)
);

ALTER TABLE ClientClaims
ADD CONSTRAINT PK_ClientClaims PRIMARY KEY (Id);

ALTER TABLE ClientClaims
ADD CONSTRAINT FK_ClientId_ClientClaims FOREIGN KEY (ClientId) REFERENCES Client(Id);