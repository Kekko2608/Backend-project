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
    V.DataViolazione BETWEEN '2009-02-01' AND '2009-07-31'

