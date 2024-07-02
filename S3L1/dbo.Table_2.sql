CREATE TABLE [dbo].Cantiere
(
	[IdCantiere] INT NOT NULL PRIMARY KEY identity, 
    [Denominazionecantiere] NVARCHAR(50) NULL, 
    [Indirizzo] NVARCHAR(50) NULL, 
    [Citta] NVARCHAR(50) NULL, 
    [CAP] INT NULL, 
    [PersonaRiferimento] NVARCHAR(50) NULL, 
    [DataInizioLavori] DATETIME2 NULL, 
    [DataFineLavori] DATETIME2 NULL, 
    [LavoriTerminatiSI_NO] BIT NULL
)
