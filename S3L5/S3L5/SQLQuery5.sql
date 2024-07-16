SELECT 
    A.Nome AS 'Nome',
    A.Cognome AS 'Cognome',
    COUNT(V.IdVerbale) AS 'Numero Verbali'
FROM 
   VERBALE V
JOIN 
    ANAGRAFICA A ON V.FK_Anagrafica = A.IdAnagrafica

GROUP BY 
    A.Nome, A.Cognome

