SELECT 
    Nome, Cognome
FROM 
    IMPIEGATO
WHERE 
    Cognome LIKE 'G%'
ORDER BY 
    Cognome, Nome;
