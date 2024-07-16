SELECT 
    A.Nome,
    A.Cognome,
    SUM(V.DecurtamentoPunti ) AS 'Totale Punti Decurtati'
FROM 
    VERBALE V
JOIN 
    ANAGRAFICA A ON V.FK_Anagrafica = A.IdAnagrafica

GROUP BY 
    A.Nome, A.Cognome

