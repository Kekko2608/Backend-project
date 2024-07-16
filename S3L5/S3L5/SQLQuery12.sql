SELECT 
    DataViolazione,
    Importo,
    DecurtamentoPunti
FROM 
    VERBALE 
WHERE 
CONVERT(DATE, DataViolazione) = '01/06/2023';
