select
	o.CustomerID,sum(Freight) as 'totale spesa'
from 
	orders as o
group by
	o.CustomerID