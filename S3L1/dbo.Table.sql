CREATE TABLE [dbo].Cliente
(
	[IdCliente] INT NOT NULL PRIMARY KEY identity, 
    [Nome] NVARCHAR(50) NOT NULL, 
    [Cognome] NVARCHAR(50) NOT NULL, 
    [DataNascita] DATETIME2 NOT NULL, 
    [Sesso] NCHAR(2) NULL DEFAULT 'm', 
    [CF] NCHAR(16) NULL, 
    [P.IVA] CHAR(11) NULL, 
    [Attivo] BIT NULL DEFAULT 1, 
    [Saldo] DECIMAL(18, 2) NULL 
)
