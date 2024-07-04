SELECT 
    i.Nome, 
    i.Cognome, 
    imp.Assunzione
FROM 
    IMPIEGO imp
JOIN 
    IMPIEGATO i ON imp.IDImpiego = i.IDImpiego
WHERE 
    imp.Assunzione BETWEEN '2017-01-01' AND '2018-01-01';
