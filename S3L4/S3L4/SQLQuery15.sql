DECLARE @TipoImpiego NVARCHAR(50) = 'Manager'; -- Specifica il tipo di impiego desiderato

SELECT 
    i.Nome, 
    i.Cognome, 
    i.[Reddito Mensile], 
    imp.[Tipo impiego] AS TipoImpiego
FROM 
    IMPIEGATO i
JOIN 
    IMPIEGO imp ON i.IDImpiego = imp.IDImpiego
WHERE 
    imp.[Tipo impiego] = @TipoImpiego;
