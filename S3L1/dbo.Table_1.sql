CREATE TABLE [dbo].Operaio
(
	[IdDipendente] INT NOT NULL PRIMARY KEY identity, 
    [Nome] NVARCHAR(50) NOT NULL, 
    [Cognome] NVARCHAR(50) NOT NULL, 
    [CF] NCHAR(16) NOT NULL, 
    [FigliACarico] INT NULL, 
    [Sposato] BIT NULL, 
    [LivelloLavorativo] INT NULL, 
    [DescrizioneMansione] NVARCHAR(50) NULL, 
    [Salario] DECIMAL(18, 2) NULL, 
    [IDCantiereFK] INT NOT NULL
)
