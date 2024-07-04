SELECT 
    Nome, 
    Cognome, 
    [Reddito Mensile]
FROM 
    IMPIEGATO
WHERE 
    [Reddito Mensile] = (SELECT MAX([Reddito Mensile]) FROM IMPIEGATO);
