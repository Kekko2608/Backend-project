SELECT 
    V.Descrizione AS 'Tipo Violazione',
    COUNT(VER.IdVerbale) AS 'Numero verbali trascritti'
FROM 
    VERBALE VER
JOIN 
    VIOLAZIONE V ON VER.FK_Violazione = V.IdViolazione
GROUP BY 
    V.Descrizione

