SELECT 
    A.Nome,
    A.Cognome,
    V.DataViolazione,
    V.Importo,
    V.DecurtamentoPunti
FROM 
    VERBALE V
JOIN 
   ANAGRAFICA A ON V.FK_Anagrafica = A.IdAnagrafica
WHERE
    A.Citta = 'Palermo';
