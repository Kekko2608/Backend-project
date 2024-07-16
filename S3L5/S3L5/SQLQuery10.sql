SELECT
	A.Nome,
    A.Cognome,
	SUM(Importo) AS 'Importo Totale'
FROM
	VERBALE V 
JOIN
	ANAGRAFICA A ON V.FK_Anagrafica=A.IdAnagrafica
GROUP BY 
    A.Nome, A.Cognome