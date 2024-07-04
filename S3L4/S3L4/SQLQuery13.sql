SELECT 
    Nome, 
    Cognome, 
    [Reddito Mensile]
FROM 
    IMPIEGATO
WHERE 
    [Reddito Mensile] = (SELECT MIN([Reddito Mensile]) FROM IMPIEGATO);