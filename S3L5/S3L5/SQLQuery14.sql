SELECT 
    A.Cognome,
    A.Nome,
    A.Indirizzo,
    V.DataViolazione,
    V.Importo,
    V.DecurtamentoPunti
FROM 
    VERBALE V
JOIN 
    ANAGRAFICA A ON V.FK_Anagrafica = A.IdAnagrafica
WHERE 
    V.DecurtamentoPunti > 5;
