SELECT 
    NominativoAgente,
    COUNT(IdVerbale) AS 'Numero Violazioni'
FROM 
   VERBALE 
GROUP BY 
    NominativoAgente
